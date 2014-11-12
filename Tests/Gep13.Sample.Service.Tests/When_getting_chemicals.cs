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
        public void Should_get_chemical_by_id()
        {
            fakeChemicalRepository.GetById(1).Returns(new Chemical { Id = 1 });

            var checmical = chemicalService.GetChemicalById(1);

            Assert.That(checmical.Id, Is.EqualTo(1));

            fakeChemicalRepository.Received().GetById(1);
        }

        [Test]
        public void Shoud_get_chemicals()
        {
            fakeChemicalRepository.GetAll().Returns(chemicals);

            var actual = chemicalService.GetChemicals();

            Assert.That(actual.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Should_get_chemical_by_name()
        {
            const string ChemicalName = "First";

            
            fakeChemicalRepository.GetByName("First").ReturnsForAnyArgs(chemicals.Where(x => x.Name == ChemicalName));

            var result = chemicalService.GetChemicalByName(ChemicalName).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(ChemicalName));
        }

        [Test]
        public void Should_return_empty_IEnumerable()
        {
            const string Missing = "Missing";

            fakeChemicalRepository.GetByName("Missing").Returns(chemicals.Where(x => x.Name == Missing));

            var result = chemicalService.GetChemicalByName(Missing);

            Assert.That(result, Is.Empty);
        }
    }
}