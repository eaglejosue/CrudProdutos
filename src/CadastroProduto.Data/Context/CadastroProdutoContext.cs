using CadastroProduto.Data.Mappings;
using CadastroProduto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroProduto.Data.Context
{
    public class CadastroProdutoContext : DbContext
    {
        public CadastroProdutoContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfiguration(new ProdutoMap());

        public DbSet<Produto> Produtos {get; set; }
    }
}