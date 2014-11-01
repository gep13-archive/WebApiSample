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

        public Gep13Context GetContext()
        {
            return dataContext ?? (dataContext = new Gep13Context());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }
    }
}