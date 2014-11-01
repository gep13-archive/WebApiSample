﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_creating_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_creating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_chemicals : CommonTestSetup
    {
        [Test]
        public void Should_create_chemical()
        {
            fakeChemicalRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical>());

            fakeChemicalRepository.Add(Arg.Do<Chemical>(x => x.Id = 1)).Returns(new Chemical { Id = 1 });

            var chemical = chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical.Id, Is.EqualTo(1));
            fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_return_null_if_chemical_with_same_name_already_exists()
        {
            fakeChemicalRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical> { new Chemical { Id = 1 } });

            var chemical = chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical, Is.Null);
        }
    }
}
