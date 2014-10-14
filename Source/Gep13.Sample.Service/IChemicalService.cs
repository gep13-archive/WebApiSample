// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChemicalService.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the IChemicalService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    using System.Collections.Generic;

    using Gep13.Sample.Model;

    public interface IChemicalService
    {
        IEnumerable<ChemicalDTO> GetChemicals();

        ChemicalDTO AddChemical(string name, double balance);

        ChemicalDTO GetChemicalById(int id);

        IEnumerable<ChemicalDTO> GetChemicalByName(string name);

        bool UpdateChemical(ChemicalDTO chemical);

        void DeleteChemical(int chemicalId);

        bool ArchiveChemical(int id);
    }
}