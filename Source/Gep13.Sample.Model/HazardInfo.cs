// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HazardInfo.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the HazardInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("HazardInfos")]
    public class HazardInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChemicalId { get; set; }

        public string Name { get; set; }

        public string Danger { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Chemical Chemical { get; set; }
    }
}