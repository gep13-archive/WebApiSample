// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalService.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    using System.Collections.Generic;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    public class ChemicalService : IChemicalService
    {
        private readonly IChemicalRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ChemicalService(IChemicalRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Chemical> GetChemicals()
        {
            return this.repository.GetAll();
        }

        public Chemical AddChemical(Chemical chemical)
        {
            this.repository.Add(chemical);
            this.SaveChanges();
            return chemical;
        }

        public Chemical GetChemicalById(int id)
        {
            return this.repository.GetById(id);
        }

        public IEnumerable<Chemical> GetChemicalByName(string name)
        {
            return this.repository.GetMany(s => s.Name == name);
        }

        public Chemical UpdateChemical(Chemical chemical)
        {
            this.repository.Update(chemical);
            this.SaveChanges();
            return chemical;
        }

        public void DeleteChemical(int chemicalId)
        {
            var chemical = this.repository.GetById(chemicalId);
            if (chemical != null)
            {
                this.repository.Delete(chemical);
                this.SaveChanges();
            }
        }

        private void SaveChanges()
        {
            this.unitOfWork.SaveChanges();
        }
    }
}