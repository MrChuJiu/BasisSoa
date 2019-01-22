using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {

            return new MapperConfiguration(cfg =>
            {
                //实体转视图
                cfg.AddProfile(new EntityToViewModelMappingProfile());
                //视图转实体
                cfg.AddProfile(new ViewModelToEntityMappingProfile());
            });
        }
    }
}
