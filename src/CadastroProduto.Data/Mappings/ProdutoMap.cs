using CadastroProduto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroProduto.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(150)");
            builder.Property(p => p.ImagemURL).HasColumnType("varchar(250)");
            builder.Property(p => p.Valor).IsRequired().HasColumnType("numeric(38,2)");
            builder.Property(p => p.DataCriacao).IsRequired().HasColumnType("datetime");

            builder.HasData(
                new Produto(1, "Produto A", 11m),
                new Produto(2, "Produto B", 21m),
                new Produto(3, "Produto C", 13m),
                new Produto(4, "Produto D", 45m),
                new Produto(5, "Produto E", 500m),
                new Produto(6, "Produto F", 1.5m),
                new Produto(7, "Produto G", 2.5m),
                new Produto(8, "Produto H", 25.20m),
                new Produto(9, "Produto I", 19.5m),
                new Produto(10, "Produto J", 10m)
            );
        }
    }
}