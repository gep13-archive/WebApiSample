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

        public IEnumerable<ChemicalDto> GetChemicals()
        {
            var chemicals = repository.GetAll();
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDto>>(chemicals);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This needs to be looked at")]
        public ChemicalDto AddChemical(string name, string code, double balance)
        {
            if (GetByName(name).Any())
            {
                return null;
            }

            if (this.GetByCode(code).Any())
            {
                return null;
            }

            var entity = new Chemical { Name = name, Code = code, Balance = balance };

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

        public ChemicalDto GetChemicalById(int id)
        {
            return Mapper.Map<Chemical, ChemicalDto>(GetById(id));
        }

        public IEnumerable<ChemicalDto> GetChemicalByName(string name)
        {
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDto>>(GetByName(name));
        }

        public bool UpdateChemical(ChemicalDto chemicalDto)
        {
            if (chemicalDto == null)
            {
                throw new ArgumentNullException("chemicalDto");
            }

            var chemical = this.GetById(chemicalDto.Id);

            if (chemical == null)
            {
                return false;
            }

            if (this.GetByName(chemicalDto.Name).Any(c => c.Id != chemicalDto.Id))
            {
                return false;
            }

            if (this.GetByCode(chemicalDto.Code).Any(c => c.Id != chemicalDto.Id))
            {
                return false;
            }

            Mapper.Map(chemicalDto, chemical);
            this.repository.Update(chemical);
            this.SaveChanges();
            return true;
        }

        public bool DeleteChemical(int id)
        {
            var chemical = this.GetById(id);

            if (chemical == null)
            {
                return false;
            }

            this.repository.Delete(chemical);
            this.SaveChanges();
            return true;
        }

        public bool ArchiveChemical(int id)
        {
            var chemical = this.GetById(id);

            if (chemical == null)
            {
                return false;
            }

            chemical.IsArchived = true;
            this.repository.Update(chemical);
            this.SaveChanges();
            return true;
        }

        private IEnumerable<Chemical> GetByName(string name)
        {
            return repository.GetMany(s => s.Name == name);
        }

        private IEnumerable<Chemical> GetByCode(string code)
        {
            return this.repository.GetMany(s => s.Code == code);
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