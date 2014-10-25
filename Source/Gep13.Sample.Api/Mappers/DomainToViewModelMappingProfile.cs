// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainToViewModelMappingProfile.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the DomainToViewModelMappingProfile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Mappers
{
    using AutoMapper;

    using Gep13.Sample.Api.ViewModels;
    using Gep13.Sample.Model;
    using Gep13.Sample.Service;

    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappingProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ChemicalDto, ChemicalViewModel>();
            Mapper.CreateMap<Chemical, ChemicalDto>();
        }
    }
}