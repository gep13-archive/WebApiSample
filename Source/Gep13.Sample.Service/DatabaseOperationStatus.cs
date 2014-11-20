// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseOperationStatus.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the DatabaseOperationStatus type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service
{
    using System.ComponentModel;

    public enum DatabaseOperationStatus
    {
        [Description("Everything worked as expected")]
        Success,
        [Description("Underlying data has changed, unable to update")]
        ConcurrencyProblem,
        [Description("General Exception was thrown")]
        Exception,
        [Description("Unable to find the entity that was requested")]
        NotFound,
        [Description("Entity already exists")]
        Conflict
    }
}