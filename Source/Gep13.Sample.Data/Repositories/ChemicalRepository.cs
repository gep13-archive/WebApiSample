// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalRepository.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Repositories
{
    using System.Collections.Generic;
    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Model;
    using Simple.Data;

    public class ChemicalRepository : IChemicalRepository
    {
        private dynamic db;

        public ChemicalRepository() 
        {
            db = Database.Open();
        }

        public Chemical GetById(int id) 
        {
            return db.Checmicals.FindById(id);
        }

        public IEnumerable<Chemical> GetAll() 
        {
            return db.Chemicals.All().ToList();
        }

        public IEnumerable<Chemical> GetByName(string name) 
        {
            return db.Chemicals.FindAllByName(name).ToList<Chemical>();
        }

        public IEnumerable<Chemical> GetByCode(string code) 
        {
            return db.Chemicals.FindByCode(code).ToList<Chemical>();
        }

        public Chemical Insert(Chemical chemical) 
        {
            return db.Checmicals.Insert(chemical);
        }

        public Chemical Update(Chemical chemical) 
        {
            return db.Chemicals.Update(chemical);
        }

        public void Delete(Chemical chemical) 
        {
            db.Chemicals.DeleteById(chemical.Id);
        }
    }
}