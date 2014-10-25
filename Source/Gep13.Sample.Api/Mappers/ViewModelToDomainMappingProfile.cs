// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelToDomainMappingProfile.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ViewModelToDomainMappingProfile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Mappers
{
    using AutoMapper;

    using Gep13.Sample.Api.ViewModels;
    using Gep13.Sample.Model;
    using Gep13.Sample.Service;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelToDomainMappingProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ChemicalViewModel, ChemicalDto>();
            Mapper.CreateMap<ChemicalDto, Chemical>();
        }
    }
}