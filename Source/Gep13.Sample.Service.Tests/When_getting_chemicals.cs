// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_getting_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_getting_chemicals type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_getting_chemicals
    {
        private IChemicalRepository fakeChemicalRepository;
        private IUnitOfWork fakeUnitOfWork;
        private ChemicalService chemicalService;
        private List<Chemical> chemicals;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Mapper.CreateMap<Chemical, ChemicalDto>();
        }

        [SetUp]
        public void SetUp()
        {
            this.fakeChemicalRepository = Substitute.For<IChemicalRepository>();
            this.fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            this.chemicalService = new ChemicalService(this.fakeChemicalRepository, this.fakeUnitOfWork);
            this.chemicals = new List<Chemical>
                                 {
                                     { new Chemical { Id = 1, Name = "First" } },
                                     { new Chemical { Id = 2, Name = "Second" } },
                                     { new Chemical { Id = 3, Name = "Third" } }
                                 };
        }

        [Test]
        public void Should_get_chemical_by_id()
        {
            this.fakeChemicalRepository.GetById(1).Returns(new Chemical { Id = 1 });

            var checmical = this.chemicalService.GetChemicalById(1);

            Assert.That(checmical.Id, Is.EqualTo(1));

            this.fakeChemicalRepository.Received().GetById(1);
        }

        [Test]
        public void Shoud_get_chemicals()
        {
            this.fakeChemicalRepository.GetAll().Returns(this.chemicals);

            var actual = this.chemicalService.GetChemicals();

            Assert.That(actual.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Should_get_chemical_by_name()
        {
            const string ChemicalName = "First";

            // Don't like this! First time I've found a problem with NSubstitute trying to mock the Expression<func<Checmical,bool>>
            this.fakeChemicalRepository.GetMany(x => x.Name == Arg.Is(ChemicalName)).ReturnsForAnyArgs(this.chemicals.Where(x => x.Name == ChemicalName));

            var result = this.chemicalService.GetChemicalByName(ChemicalName).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(ChemicalName));
        }

        [Test]
        public void Should_return_empty_IEnumerable()
        {
            const string Missing = "Missing";

            this.fakeChemicalRepository.GetMany(x => x.Name == Arg.Is(Missing)).ReturnsForAnyArgs(this.chemicals.Where(x => x.Name == Missing));

            var result = this.chemicalService.GetChemicalByName(Missing);

            Assert.That(result, Is.Empty);
        }
    }
}