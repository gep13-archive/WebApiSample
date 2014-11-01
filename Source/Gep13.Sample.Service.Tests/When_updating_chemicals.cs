// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_updating_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_updating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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

            var actual = chemicalService.UpdateChemical(toUpdate);

            fakeChemicalRepository.Received().Update(Arg.Any<Chemical>());
            fakeUnitOfWork.Received().SaveChanges();
        }
    }
}
