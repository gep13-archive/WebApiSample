// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseFactory.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the DatabaseFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private Gep13Context dataContext;

        public Gep13Context Get()
        {
            return this.dataContext ?? (this.dataContext = new Gep13Context());
        }

        protected override void DisposeCore()
        {
            if (this.dataContext != null)
            {
                this.dataContext.Dispose();
            }
        }
    }
}