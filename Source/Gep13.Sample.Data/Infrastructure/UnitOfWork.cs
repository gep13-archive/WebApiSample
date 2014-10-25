// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the UnitOfWork type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private Gep13Context gep13Context;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        private Gep13Context DbContext
        {
            get
            {
                return this.gep13Context ?? this.databaseFactory.GetContext();
            }
        }

        public void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }
    }
}