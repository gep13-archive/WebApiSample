// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_deleting_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_deleting_chemicals type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Tests 
{
    using System.Runtime;

    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_deleting_chemicals : CommonTestSetup
    {
        [Test]
        public void Should_return_success()
        {
            var fakeChemical = new Chemical { Id = 1 };

            fakeChemicalRepository.GetById(Arg.Any<int>()).Returns(fakeChemical);

            var databaseOperationStatus = chemicalService.DeleteChemical(1);

            Assert.That(databaseOperationStatus, Is.EqualTo(DatabaseOperationStatus.Success));
            fakeChemicalRepository.Received().Delete(fakeChemical);
            fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_return_notfound_if_unable_to_find_entityt()
        {
            fakeChemicalRepository.GetById(Arg.Any<int>()).Returns(r => null);

            var databaseOperationStatus = chemicalService.DeleteChemical(1);

            Assert.That(databaseOperationStatus, Is.EqualTo(DatabaseOperationStatus.NotFound));
            fakeChemicalRepository.DidNotReceive().Delete(Arg.Any<Chemical>());
            fakeUnitOfWork.DidNotReceive().SaveChanges();
        }         
    }
}