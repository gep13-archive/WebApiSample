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

    public interface IChemicalService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not applicable")]
        IEnumerable<ChemicalDto> GetChemicals();

        ChemicalDto AddChemical(string name, double balance);

        ChemicalDto GetChemicalById(int id);

        IEnumerable<ChemicalDto> GetChemicalByName(string name);

        bool UpdateChemical(ChemicalDto chemical);

        bool DeleteChemical(int id);

        bool ArchiveChemical(int id);
    }
}