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
    using System;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This needs to be looked at")]
        public ChemicalDto AddChemical(string name, double balance)
        {
            if (GetByName(name).Any())
            {
                return null;
            }

            var entity = new Chemical { Name = name, Balance = balance };

            try
            {
                repository.Add(entity);
                SaveChanges();
                return Mapper.Map<Chemical, ChemicalDto>(entity);
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteChemical(int id)
        {
            var chemical = repository.GetById(id);
            if (chemical != null)
            {
                repository.Delete(chemical);
                SaveChanges();
                return true;
            }

            return false;
        }

        public bool ArchiveChemical(int id)
        {
            var found = GetById(id);

            if (found != null)
            {
                found.IsArchived = true;
                repository.Update(found);
                SaveChanges();
                return true;
            }

            return false;
        }

        public ChemicalDto GetChemicalById(int id)
        {
            return Mapper.Map<Chemical, ChemicalDto>(GetById(id));
        }

        public IEnumerable<ChemicalDto> GetChemicalByName(string name)
        {
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDto>>(GetByName(name));
        }

        public IEnumerable<ChemicalDto> GetChemicals()
        {
            var chemicals = repository.GetAll();
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDto>>(chemicals);
        }

        public bool UpdateChemical(ChemicalDto chemical)
        {
            if (chemical == null)
            {
                throw new ArgumentNullException("chemical");
            }

            var found = GetByName(chemical.Name);

            if (!found.Any())
            {
                var entity = Mapper.Map<ChemicalDto, Chemical>(chemical);
                repository.Update(entity);
                SaveChanges();
                return true;
            }

            return false;
        }

        private IEnumerable<Chemical> GetByName(string name)
        {
            return repository.GetMany(s => s.Name == name);
        }

        private Chemical GetById(int id) 
        {
            return repository.GetById(id);
        }

        private void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
}