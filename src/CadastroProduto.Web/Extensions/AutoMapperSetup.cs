using System.Reflection;
using CadastroProduto.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroProduto.Web.Extensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var mapper = AutoMapperConfiguration.ConfigureMappings();
            services.AddAutoMapper(x => mapper.CreateMapper(), Assembly.Load("CadastroProduto.Application"));
        }
    }
}