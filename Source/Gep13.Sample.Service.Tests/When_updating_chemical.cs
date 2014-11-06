// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_updating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_updating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Simple.Data;

namespace Gep13.Sample.Service.Test
{
    using System.Reflection;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_chemical
    {
        private static Assembly[] assemblies = { Assembly.Load("Gep13.Sample.Service") };

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Mapper.CreateMap<ChemicalDTO, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDTO>();
        }

        [Test]
        public void Should_update()
        {
            var adapter = new InMemoryAdapter();
            adapter.SetKeyColumn("Chemicals","Id");
            Database.UseMockAdapter(adapter);

            var db = Database.Open();
            db.Chemicals.Insert(new Chemical {Id = 1, Name = "First", Balance = 0.00});

            var chemicalService = new ChemicalService();

            var toUpdate = new Service.ChemicalDTO
            {
                Id = 1,
                Balance = 110.99,
                Name = "First"
            };

            Assert.That(chemicalService.UpdateChemical(toUpdate), Is.True);
            Chemical record = db.Chemicals.FindById(1);
            Assert.That(record.Balance, Is.EqualTo(110.99));
        }
    }
}