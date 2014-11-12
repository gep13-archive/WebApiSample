// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_creating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_creating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection.Emit;

namespace Gep13.Sample.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_chemical
    {
        private IChemicalRepository _fakeRepository;
        private IUnitOfWork _fakeUnitOfWork;
        private ChemicalService _chemicalService;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

            _fakeRepository = Substitute.For<IChemicalRepository>();
            _fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            _chemicalService = new ChemicalService(_fakeRepository, _fakeUnitOfWork);
            
            Mapper.CreateMap<ChemicalDto, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDto>();
        }

        [Test]
        public void Should_create_chemical()
        {

            _fakeRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical>());
            
            _fakeRepository.Add(Arg.Do<Chemical>(x => x.Id = 1)).Returns(new Chemical { Id = 1 });
            
            var chemical = _chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical.Id, Is.EqualTo(1));
            _fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_return_null_if_chemical_with_same_name_already_exists() 
        {
            _fakeRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical>{ new Chemical{Id=1}});

            var chemical = _chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical, Is.Null);
        }
    }
}