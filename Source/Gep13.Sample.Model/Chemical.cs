// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the Chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Chemicals")]
    public class Chemical
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsArchived { get; set; }

        public double Balance { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}