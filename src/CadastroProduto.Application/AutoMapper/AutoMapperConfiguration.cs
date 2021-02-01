using AutoMapper;
using CadastroProduto.Application.AutoMapper.Profiles;

namespace CadastroProduto.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new EntityToCommandProfile());
            });
            return mapperConfiguration;
        }
    }
}