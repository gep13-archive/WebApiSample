// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HazardInfoViewModel.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the HazardInfoViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.ViewModels
{
    public class HazardInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Danger { get; set; }

        public string RowVersion { get; set; }
    }
}