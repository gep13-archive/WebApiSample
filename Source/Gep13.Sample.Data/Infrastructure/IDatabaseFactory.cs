// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseFactory.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the IDatabaseFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Infrastructure
{
    using System;

    public interface IDatabaseFactory : IDisposable
    {
        Gep13Context Get();
    }
}