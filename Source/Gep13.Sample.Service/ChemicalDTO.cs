// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalDto.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalDto type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    using System.Security.AccessControl;

    public class ChemicalDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsArchived { get; set; }

        public double Balance { get; set; }

        public byte[] RowVersion { get; set; }
    }
}