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
            this.DatabaseFactory = databaseFactory;
            this.dbset = this.DataContext.Set<T>();
        }

        protected Gep13Context Gep13Context
        {
            get
            {
                return this.gep13Context;
            }

            private set
            {
                this.gep13Context = value;
            }
        }

        private IDatabaseFactory DatabaseFactory
        {
            get;
            set;
        }

        private Gep13Context DataContext
        {
            get { return this.gep13Context ?? (this.gep13Context = this.DatabaseFactory.Get()); }
        }

        public virtual T Add(T entity)
        {
            this.dbset.Add(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            this.dbset.Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.dbset.ToList();
        }

        public virtual T GetById(int id)
        {
            return this.dbset.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.dbset.Where(where).ToList();
        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            return this.dbset.Where(where).Count();
        }

        public virtual void Update(T entity)
        {
            this.dbset.Attach(entity);
            this.gep13Context.Entry(entity).State = EntityState.Modified;
        }
    }
}