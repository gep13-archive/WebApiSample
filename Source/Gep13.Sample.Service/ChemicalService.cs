// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalService.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using AutoMapper;

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

        public ChemicalViewModel AddChemical(ChemicalViewModel chemical)
        {


            var found = this.GetChemicalByName(chemical.Name);
            if (found.ToList().Count == 0)
            {
                var entity = Mapper.Map<ChemicalViewModel,Chemical>(chemical);
                try
                {
                    this.repository.Add(entity);
                    this.SaveChanges();
                   return Mapper.Map<Chemical, ChemicalViewModel>(entity);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public ChemicalViewModel GetChemicalById(int id)
        {
            return Mapper.Map<Chemical, ChemicalViewModel>(this.repository.GetById(id));
        }

        public IEnumerable<ChemicalViewModel> GetChemicalByName(string name)
        {
            return Mapper.Map<IEnumerable<Chemical>,IEnumerable<ChemicalViewModel>>(this.repository.GetMany(s => s.Name == name));
        }

        public bool UpdateChemical(ChemicalViewModel chemical)
        {

            var found = this.GetChemicalByName(chemical.Name);
            // Why does getbyname return IEnumerable?? 
            if (found.ToList().Count == 0) {
                var entity = Mapper.Map<ChemicalViewModel, Chemical>(chemical);
                this.repository.Update(entity);
                this.SaveChanges();
                return true;
            }

            return false;
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