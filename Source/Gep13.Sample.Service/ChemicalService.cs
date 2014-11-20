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
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
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

        public DatabaseOperation<IEnumerable<ChemicalDto>> GetChemicals()
        {
            var chemicals = repository.GetAll();
            return new DatabaseOperation<IEnumerable<ChemicalDto>>
                       {
                           Status = DatabaseOperationStatus.Success,
                           Result =
                               Mapper
                               .Map<IEnumerable<Chemical>, IEnumerable<ChemicalDto>>(chemicals)
                       };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This needs to be looked at")]
        public DatabaseOperation<ChemicalDto> AddChemical(string name, string code, double balance)
        {
            if (GetByName(name).Any() || this.GetByCode(code).Any())
            {
                return new DatabaseOperation<ChemicalDto>() { Status = DatabaseOperationStatus.Conflict, Result = null };
            }

            var entity = new Chemical { Name = name, Code = code, Balance = balance, HazardInfo = new HazardInfo() };

            try
            {
                repository.Add(entity);
                SaveChanges();
                return new DatabaseOperation<ChemicalDto>()
                           {
                               Status = DatabaseOperationStatus.Success,
                               Result = Mapper.Map<Chemical, ChemicalDto>(entity)
                           };
            }
            catch
            {
                return new DatabaseOperation<ChemicalDto>()
                           {
                               Status = DatabaseOperationStatus.Exception,
                               Result = null
                           };
            }
        }

        public DatabaseOperation<ChemicalDto> GetChemicalById(int id)
        {
            var chemical = this.GetById(id);
            return chemical == null
                       ? new DatabaseOperation<ChemicalDto>()
                             {
                                 Status = DatabaseOperationStatus.NotFound,
                                 Result = null
                             }
                       : new DatabaseOperation<ChemicalDto>()
                             {
                                 Status = DatabaseOperationStatus.Success,
                                 Result = Mapper.Map<Chemical, ChemicalDto>(chemical)
                             };
        }

        public DatabaseOperation<IEnumerable<ChemicalDto>> GetChemicalByName(string name)
        {
            var chemicals = this.GetByName(name);
            return new DatabaseOperation<IEnumerable<ChemicalDto>>()
                             {
                                 Status = DatabaseOperationStatus.Success,
                                 Result =
                                     Mapper
                                     .Map<IEnumerable<Chemical>, IEnumerable<ChemicalDto>>(chemicals)
                             };
        }

        public DatabaseOperation<HazardInfoDto> GetHazardInfoForChemicalId(int id)
        {
            var chemical = this.repository.GetWithHazardInfoById(id);
            return chemical == null
            ? new DatabaseOperation<HazardInfoDto>()
            {
                Status = DatabaseOperationStatus.NotFound,
                Result = null
            }
            : new DatabaseOperation<HazardInfoDto>()
            {
                Status = DatabaseOperationStatus.Success,
                Result = Mapper.Map<HazardInfo, HazardInfoDto>(chemical.HazardInfo)
            };
        }

        public DatabaseOperation<ChemicalDto> UpdateChemical(ChemicalDto chemicalDto)
        {
            if (chemicalDto == null)
            {
                throw new ArgumentNullException("chemicalDto");
            }

            var chemical = this.GetById(chemicalDto.Id);

            if (chemical == null)
            {
                return new DatabaseOperation<ChemicalDto>() { Status = DatabaseOperationStatus.NotFound, Result = null };
            }

            if (this.GetByName(chemicalDto.Name).Any(c => c.Id != chemicalDto.Id) || this.GetByCode(chemicalDto.Code).Any(c => c.Id != chemicalDto.Id))
            {
                return new DatabaseOperation<ChemicalDto>() { Status = DatabaseOperationStatus.Conflict, Result = null };
            }

            try
            {
                Mapper.Map(chemicalDto, chemical);
                this.repository.Update(chemical);
                this.SaveChanges();
                return new DatabaseOperation<ChemicalDto>()
                           {
                               Status = DatabaseOperationStatus.Success,
                               Result = Mapper.Map<Chemical, ChemicalDto>(chemical)
                           };
            }
            catch (DbUpdateConcurrencyException dbucException)
            {
                return new DatabaseOperation<ChemicalDto>()
                           {
                               Status = DatabaseOperationStatus.ConcurrencyProblem,
                               Result = null
                           };
            }
        }

        public DatabaseOperation<HazardInfoDto> UpdateHazardInfo(int chemicalId, HazardInfoDto hazardInfoDto)
        {
            if (hazardInfoDto == null)
            {
                throw new ArgumentNullException("hazardInfoDto");
            }

            var chemical = this.repository.GetWithHazardInfoById(chemicalId);

            if (chemical == null)
            {
                return new DatabaseOperation<HazardInfoDto>() { Status = DatabaseOperationStatus.NotFound, Result = null };
            }

            try
            {
                Mapper.Map(hazardInfoDto, chemical.HazardInfo);
                this.repository.Update(chemical);
                this.SaveChanges();
                return new DatabaseOperation<HazardInfoDto>()
                {
                    Status = DatabaseOperationStatus.Success,
                    Result = Mapper.Map<HazardInfo, HazardInfoDto>(chemical.HazardInfo)
                };
            }
            catch (DbUpdateConcurrencyException dbucException)
            {
                return new DatabaseOperation<HazardInfoDto>()
                {
                    Status = DatabaseOperationStatus.ConcurrencyProblem,
                    Result = null
                };
            }
        }

        public DatabaseOperationStatus DeleteChemical(int id)
        {
            var chemical = this.GetById(id);

            if (chemical == null)
            {
                return DatabaseOperationStatus.NotFound;
            }

            this.repository.Delete(chemical);
            this.SaveChanges();
            return DatabaseOperationStatus.Success;
        }

        public DatabaseOperationStatus ArchiveChemical(int id)
        {
            var chemical = this.GetById(id);

            if (chemical == null)
            {
                return DatabaseOperationStatus.NotFound;
            }

            chemical.IsArchived = true;
            this.repository.Update(chemical);
            this.SaveChanges();
            return DatabaseOperationStatus.Success;
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