// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalRepository.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Repositories
{
    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Model;

    public class ChemicalRepository : RepositoryBase<Chemical>, IChemicalRepository
    {
        public ChemicalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}