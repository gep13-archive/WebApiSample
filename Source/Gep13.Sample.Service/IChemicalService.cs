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
        IEnumerable<Chemical> GetChemicals();

        ChemicalViewModel AddChemical(ChemicalViewModel chemical);

        ChemicalViewModel GetChemicalById(int id);

        IEnumerable<ChemicalViewModel> GetChemicalByName(string name);

        bool UpdateChemical(ChemicalViewModel chemical);

        void DeleteChemical(int chemicalId);
    }
}