// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_creating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_creating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection.Emit;
using Simple.Data;

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
        private ChemicalService _chemicalService;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {           
            Mapper.CreateMap<ChemicalDTO, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDTO>();
        }

        [SetUp]
        public void SetUp()
        {
            var adapter = new InMemoryAdapter();
            adapter.SetAutoIncrementKeyColumn("Chemicals","Id");
            Database.UseMockAdapter(adapter);
            _chemicalService = new ChemicalService();            
        }

        [Test]
        public void Should_create_chemical()
        {
            
            var chemical = _chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical.Id, Is.EqualTo(1));
            
        }

        [Test]
        public void Should_return_null_if_chemical_with_same_name_already_exists()
        {
            var db = Database.Open();
            db.Chemicals.Insert(new Chemical {Name = "First"});

            var chemical = _chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical, Is.Null);
        }
    }
}