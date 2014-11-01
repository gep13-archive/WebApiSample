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
        public void Should_return_false_if_unable_to_find_chemical() 
        {
            fakeChemicalRepository.GetById(1).Returns(x => null);

            var actual = chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Should_return_true_if_archives_chemical() 
        {
            var entity = new Chemical 
            {
                Id = 1
            };

            fakeChemicalRepository.GetById(1).Returns(x => entity);

            var actual = chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.True);
            Assert.That(entity.IsArchived, Is.True);
        }
    }
}