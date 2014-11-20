// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_updating_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_updating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Gep13.Sample.Service.Tests
{
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_chemicals : CommonTestSetup
    {
        [Test]
        public void Should_return_success_and_updated_entity()
        {
            var toUpdate = new Service.ChemicalDto
            {
                Id = 1,
                Balance = 110.99,
                Name = "First"
            };

            fakeChemicalRepository.GetById(1).Returns(new Chemical { Id = 1, Balance = 100, Name = "First" });

            var databaseOperation = chemicalService.UpdateChemical(toUpdate);

            Assert.That(databaseOperation, Is.Not.Null);
            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.Success));
            fakeChemicalRepository.Received().Update(Arg.Any<Chemical>());
            fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_throw_ArgumentNullException_if_DTO_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => chemicalService.UpdateChemical(null));
        }

        [Test]
        public void Should_return_notfound_if_unable_to_find_record()
        {
            fakeChemicalRepository.GetById(1).Returns(x => null);

            var databaseOperation = chemicalService.UpdateChemical(new ChemicalDto { Id = 1 });

            Assert.That(databaseOperation, Is.Not.Null);
            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.NotFound));
        }

        [Test]
        public void Should_return_notfound_if_another_Chemical_found_with_same_name()
        {
            fakeChemicalRepository.GetMany(x => x.Name == "Test").Returns(new[] { new Chemical { Id = 2 } });

            var databaseOperation = chemicalService.UpdateChemical(new ChemicalDto { Id = 1, Name = "Test" });
            Assert.That(databaseOperation, Is.Not.Null);
            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.NotFound));
        }

        [Test]
        public void Should_return_false_if_another_chemical_found_with_same_code()
        {
            fakeChemicalRepository.GetMany(x => x.Code == "123").Returns(new[] { new Chemical { Id = 2, Code = "123" } });

            var databaseOperation = chemicalService.UpdateChemical(new ChemicalDto { Id = 1, Code = "123" });

            Assert.That(databaseOperation, Is.Not.Null);
            Assert.That(databaseOperation.Status, Is.EqualTo(DatabaseOperationStatus.NotFound));
        }
    }
}
