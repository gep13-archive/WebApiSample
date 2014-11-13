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
            return db.Chemicals.FindById(id);
        }

        public IEnumerable<Chemical> GetAll() 
        {
            return db.Chemicals.All().ToList<Chemical>();
        }

        public IEnumerable<Chemical> GetByName(string name) 
        {
            return db.Chemicals.FindAllByName(name).ToList<Chemical>();
        }

        public IEnumerable<Chemical> GetByCode(string code) 
        {
            return db.Chemicals.FindAllByCode(code).ToList<Chemical>();
        }

        public Chemical Insert(Chemical chemical) 
        {
            return db.Chemicals.Insert(chemical);
        }

        public void Update(Chemical chemical) 
        {
            db.Chemicals.Update(chemical);
        }

        public void Delete(Chemical chemical) 
        {
            db.Chemicals.DeleteById(chemical.Id);
        }
    }
}