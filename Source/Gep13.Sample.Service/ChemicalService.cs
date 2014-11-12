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

        public ChemicalService(IChemicalRepository repository)
        {
            this.repository = repository;
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

            if (GetByCode(code).Any())
            {
                return null;
            }

            var entity = new Chemical { Name = name, Code = code, Balance = balance };

            try
            {
                repository.Insert(entity);
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
            return true;
        }

        private IEnumerable<Chemical> GetByName(string name)
        {
            return repository.GetByName(name);
        }

        private IEnumerable<Chemical> GetByCode(string code)
        {
            return this.repository.GetByCode(code);
        }

        private Chemical GetById(int id) 
        {
            return repository.GetById(id);
        }
    }
}