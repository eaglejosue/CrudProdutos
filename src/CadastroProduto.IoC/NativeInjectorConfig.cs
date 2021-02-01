using CadastroProduto.Data.Repositories;
using CadastroProduto.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroProduto.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}