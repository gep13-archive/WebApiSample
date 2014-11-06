// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_deleting_chemicals.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_deleting_chemicals type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Simple.Data;

namespace Gep13.Sample.Service.Test 
{
    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_deleting_chemicals 
    {
        private ChemicalService chemicalService;
        
        [SetUp]
        public void SetUp()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
            this.chemicalService = new ChemicalService();
        }

        [Test]
        public void Should_delete_chemical()
        {
            var db = Database.Open();
            db.Chemicals.Insert(new Chemical {Id = 1});

            Assert.That(chemicalService.DeleteChemical(1), Is.True);
        }

        [Test]
        public void Should_not_delete_chemical_if_not_found()
        {
            Assert.That(chemicalService.DeleteChemical(1), Is.False);
        }         
    }
}