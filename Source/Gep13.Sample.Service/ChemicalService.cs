// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalService.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Simple.Data;

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
        private readonly dynamic _db;

        public ChemicalService()
        {
            _db = Database.OpenNamedConnection("Gep13");
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
                _db.Chemicals.Insert(entity);
                return Mapper.Map<Chemical, ChemicalDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteChemical(int id)
        {
            var chemical = GetById(id);
            if (chemical != null)
            {
                _db.Chemicals.DeleteById(id);
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
                _db.Chemicals.UpdateById(found);
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
            var chemicals = _db.Chemicals.GetAll();
            return Mapper.Map<IEnumerable<Chemical>, IEnumerable<ChemicalDTO>>(chemicals);
        }

        public bool UpdateChemical(ChemicalDTO chemical)
        {
            var found = this.GetByName(chemical.Name);

            if (!found.Any())
            {
                var entity = Mapper.Map<ChemicalDTO, Chemical>(chemical);
                _db.Chemicals.UpdateById(entity);
                return true;
            }

            return false;
        }

        private IEnumerable<Chemical> GetByName(string name)
        {
            return _db.Chemicals.GetAllBy(Name: name).Cast<Chemical>();
        }

        private Chemical GetById(int id) 
        {
            return _db.Chemicals.GetById(id).Cast<Chemical>();
        }

    }
}