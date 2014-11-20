// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_archiving_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_creating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Tests
{
    using Gep13.Sample.Model;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class When_archiving_chemicals : CommonTestSetup
    {
        [Test]
        public void Should_return_notfound_if_unable_to_find_entity() 
        {
            fakeChemicalRepository.GetById(1).Returns(x => null);

            var databaseOperationStatus = chemicalService.ArchiveChemical(1);

            Assert.That(databaseOperationStatus, Is.EqualTo(DatabaseOperationStatus.NotFound));
        }

        [Test]
        public void Should_return_success() 
        {
            var entity = new Chemical 
            {
                Id = 1
            };

            fakeChemicalRepository.GetById(1).Returns(x => entity);

            var databaseOperationStatus = chemicalService.ArchiveChemical(1);

            Assert.That(databaseOperationStatus, Is.EqualTo(DatabaseOperationStatus.Success));
        }
    }
}