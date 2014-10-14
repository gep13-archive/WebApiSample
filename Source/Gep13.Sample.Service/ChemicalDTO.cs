// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalDTO.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    public class ChemicalDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public double Balance { get; set; }
    }
}