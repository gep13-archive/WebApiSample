// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseOperation.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the DatabaseOperation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    public class DatabaseOperation<T>
    {
        public DatabaseOperationStatus Status { get; set; }

        public T Result { get; set; }
    }
}