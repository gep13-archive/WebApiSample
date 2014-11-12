// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChemicalRepository.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the IChemicalRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Repositories
{
    using System.Collections.Generic;
    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Model;

    public interface IChemicalRepository 
    {
        Chemical GetById(int id);

        IEnumerable<Chemical> GetAll();

        IEnumerable<Chemical> GetByName(string name);

        IEnumerable<Chemical> GetByCode(string code);

        Chemical Update(Chemical chemical);
        
        Chemical Insert(Chemical chemical);
        
        void Delete(Chemical chemical);
    }
}