// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gep13Context.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the Gep13Context type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data
{
    using System.Data.Entity;

    using Gep13.Sample.Model;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class Gep13Context : IdentityDbContext<IdentityUser>
    {
        public Gep13Context()
            : base("Gep13")
        {
        }

        public DbSet<Chemical> Chemicals { get; set; }

        public DbSet<HazardInfo> HazardInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chemical>().HasOptional(e => e.HazardInfo).WithRequired(e => e.Chemical);
        }
    }
}
