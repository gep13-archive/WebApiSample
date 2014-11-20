// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_getting_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_getting_chemicals type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_getting_chemicals : CommonTestSetup
    {
        private List<Chemical> chemicals;

        [SetUp]
        public void SetUp()
        {
            chemicals = new List<Chemical>
                                 {
                                     { new Chemical { Id = 1, Name = "First" } },
                                     { new Chemical { Id = 2, Name = "Second" } },
                                     { new Chemical { Id = 3, Name = "Third" } }
                                 };
        }

        [Test]
        public void Should_return_success_and_chemical_by_id()
        {
            fakeChemicalRepository.GetById(1).Returns(new Chemical { Id = 1 });

            var databaseOperation = chemicalService.GetChemicalById(1);

            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.Success));
            Assert.That(databaseOperation.Result.Id, Is.EqualTo(1));
            fakeChemicalRepository.Received().GetById(1);
        }

        [Test]
        public void Shoud_return_success()
        {
            fakeChemicalRepository.GetAll().Returns(chemicals);

            var databaseOperation = chemicalService.GetChemicals();

            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.Success));
            Assert.That(databaseOperation.Result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Should_return_success_and_get_chemical_by_name()
        {
            const string ChemicalName = "First";

            // Don't like this! First time I've found a problem with NSubstitute trying to mock the Expression<func<Checmical,bool>>
            fakeChemicalRepository.GetMany(x => x.Name == Arg.Is(ChemicalName)).ReturnsForAnyArgs(chemicals.Where(x => x.Name == ChemicalName));

            var databaseOperation = chemicalService.GetChemicalByName(ChemicalName);

            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.Success));
            Assert.That(databaseOperation.Result.ToList().Count(), Is.EqualTo(1));
            Assert.That(databaseOperation.Result.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(databaseOperation.Result.ToList()[0].Name, Is.EqualTo(ChemicalName));
        }

        [Test]
        public void Should_return_empty_IEnumerable()
        {
            const string Missing = "Missing";

            fakeChemicalRepository.GetMany(x => x.Name == Arg.Is(Missing)).ReturnsForAnyArgs(chemicals.Where(x => x.Name == Missing));

            var databaseOperation = chemicalService.GetChemicalByName(Missing);

            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.Success));
            Assert.That(databaseOperation.Result, Is.Empty);
        }
    }
}