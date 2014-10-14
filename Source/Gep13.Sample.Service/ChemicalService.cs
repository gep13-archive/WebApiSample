// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalService.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.Design;

namespace Gep13.Sample.Service
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

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

        public ChemicalDTO AddChemical(string name, double balance)
        {
            if (this.GetByName(name).Any())
            {
                return null;
            }

            var entity = new Chemical { Name = name, Balance = balance };

            try
            {
                this.repository.Add(entity);
                this.SaveChanges();
                return Mapper.Map<Chemical, ChemicalDTO>(entity);
            }
            catch
            {
                return null;
            }
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

        public bool ArchiveChemical(int id)
        {
            var found = this.GetById(id);

            if (found != null)
            {
                found.IsArchived = true;
                this.repository.Update(found);
                this.unitOfWork.SaveChanges();
                return true;
            }

            return false;
        }

        public ChemicalDTO GetChemicalById(int id)
        {
            return Mapper.Map<Chemical, ChemicalDTO>(this.GetById(id));
        }

        public IEnumerable<ChemicalDTO> GetChemicalByName(string name)
        {
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDTO>>(this.GetByName(name));
        }

        public IEnumerable<ChemicalDTO> GetChemicals()
        {
            var chemicals = this.repository.GetAll();
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDTO>>(chemicals);
        }

        public bool UpdateChemical(ChemicalDTO chemical)
        {
            var found = this.GetByName(chemical.Name);

            if (!found.Any())
            {
                var entity = Mapper.Map<ChemicalDTO, Chemical>(chemical);
                this.repository.Update(entity);
                this.SaveChanges();
                return true;
            }

            return false;
        }

        private IEnumerable<Chemical> GetByName(string name)
        {
            return this.repository.GetMany(s => s.Name == name);
        }

        private Chemical GetById(int id) 
        {
            return this.repository.GetById(id);
        }

        private void SaveChanges()
        {
            this.unitOfWork.SaveChanges();
        }
    }
}