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
        DatabaseOperation<IEnumerable<ChemicalDto>> GetChemicals();

        DatabaseOperation<ChemicalDto> AddChemical(string name, string code, double balance);

        DatabaseOperation<ChemicalDto> GetChemicalById(int id);

        DatabaseOperation<IEnumerable<ChemicalDto>> GetChemicalByName(string name);

        DatabaseOperation<HazardInfoDto> GetHazardInfoForChemicalId(int id);

        DatabaseOperation<ChemicalDto> UpdateChemical(ChemicalDto chemicalDto);

        DatabaseOperation<HazardInfoDto> UpdateHazardInfo(int chemicalId, HazardInfoDto hazardInfoDto);

        DatabaseOperationStatus DeleteChemical(int id);

        DatabaseOperationStatus ArchiveChemical(int id);
    }
}