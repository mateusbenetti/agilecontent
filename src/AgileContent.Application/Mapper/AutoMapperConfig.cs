﻿using AutoMapper;

namespace AgileContent.Application.Mapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(new DomainToViewModelMappingProfile());
                config.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
