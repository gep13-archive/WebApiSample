// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalViewModel.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.ViewModels
{
    public class ChemicalViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsArchived { get; set; }

        public double Balance { get; set; }
    }
}