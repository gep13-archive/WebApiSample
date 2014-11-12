// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_updating_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_updating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Gep13.Sample.Service.Tests
{
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_chemicals : CommonTestSetup
    {
        [Test]
        public void Should_update()
        {
            var toUpdate = new Service.ChemicalDto
            {
                Id = 1,
                Balance = 110.99,
                Name = "First"
            };

            fakeChemicalRepository.GetById(1).Returns(new Chemical {Id = 1, Balance = 100, Name = "First"});

            var actual = chemicalService.UpdateChemical(toUpdate);

            Assert.That(actual, Is.True);
            fakeChemicalRepository.Received().Update(Arg.Any<Chemical>());
            fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_throw_ArgumentNullException_if_DTO_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => chemicalService.UpdateChemical(null));
        }

        [Test]
        public void Should_return_false_if_unable_to_find_record()
        {
            fakeChemicalRepository.GetById(1).Returns(x => null);

            Assert.That(()=> chemicalService.UpdateChemical(new ChemicalDto{Id = 1}),Is.False);
        }

        [Test]
        public void Should_return_false_if_another_Chemical_found_with_same_name()
        {
            fakeChemicalRepository.GetByName("Test").Returns( new [] {new Chemical {Id = 2}});

            Assert.That(()=> chemicalService.UpdateChemical(new ChemicalDto{Id = 1, Name = "Test"}),Is.False);
        }

        [Test]
        public void Should_return_false_if_another_chemical_found_with_same_code()
        {
            fakeChemicalRepository.GetByName(Arg.Any<string>()).Returns(x => new List<Chemical>());
            fakeChemicalRepository.GetByCode("123").Returns(new[] {new Chemical {Id = 2, Code = "123"}});

            Assert.That(() => chemicalService.UpdateChemical(new ChemicalDto{Id=1, Code = "123"}), Is.False);
        }
    }
}
