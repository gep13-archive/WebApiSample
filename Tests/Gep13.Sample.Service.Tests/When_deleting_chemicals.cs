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
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_deleting_chemicals : CommonTestSetup
    {
        [Test]
        public void Should_delete_chemical()
        {
            var fakeChemical = new Chemical { Id = 1 };

            fakeChemicalRepository.GetById(Arg.Any<int>()).Returns(fakeChemical);

            chemicalService.DeleteChemical(1);

            fakeChemicalRepository.Received().Delete(fakeChemical);
            fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_not_delete_chemical_if_not_found()
        {
            fakeChemicalRepository.GetById(Arg.Any<int>()).Returns(r => null);

            chemicalService.DeleteChemical(1);

            fakeChemicalRepository.DidNotReceive().Delete(Arg.Any<Chemical>());
            fakeUnitOfWork.DidNotReceive().SaveChanges();
        }         
    }
}