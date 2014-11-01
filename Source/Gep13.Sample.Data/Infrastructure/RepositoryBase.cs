// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the RepositoryBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class RepositoryBase<T> where T : class, new()
    {
        private readonly IDbSet<T> dbset;
        private Gep13Context gep13Context;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected Gep13Context Gep13Context
        {
            get
            {
                return gep13Context;
            }

            private set
            {
                gep13Context = value;
            }
        }

        private IDatabaseFactory DatabaseFactory
        {
            get;
            set;
        }

        private Gep13Context DataContext
        {
            get { return gep13Context ?? (gep13Context = DatabaseFactory.GetContext()); }
        }

        public virtual T Add(T entity)
        {
            dbset.Add(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not applicable")]
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).Count();
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            gep13Context.Entry(entity).State = EntityState.Modified;
        }
    }
}