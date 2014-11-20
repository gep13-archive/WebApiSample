// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HazardInfoDto.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the HazardInfoDto type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    using System.Security.AccessControl;

    public class HazardInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Danger { get; set; }

        public byte[] RowVersion { get; set; }
    }
}