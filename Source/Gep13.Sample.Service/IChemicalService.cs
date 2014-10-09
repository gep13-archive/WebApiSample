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

        Chemical AddChemical(Chemical chemical);

        Chemical GetChemicalById(int id);

        IEnumerable<Chemical> GetChemicalByName(string name);

        Chemical UpdateChemical(Chemical chemical);

        void DeleteChemical(int chemicalId);
    }
}