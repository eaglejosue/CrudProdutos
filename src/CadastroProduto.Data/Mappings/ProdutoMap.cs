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
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(150)");
            builder.Property(p => p.ImagemURL).HasColumnType("varchar(250)");
            builder.Property(p => p.Valor).IsRequired().HasColumnType("numeric(38,2)");
            builder.Property(p => p.DataCriacao).IsRequired().HasColumnType("datetime");

            builder.HasData(
                new Produto("Produto A", 11m),
                new Produto("Produto B", 21m),
                new Produto("Produto C", 13m),
                new Produto("Produto D", 45m),
                new Produto("Produto E", 500m),
                new Produto("Produto F", 1.5m),
                new Produto("Produto G", 2.5m),
                new Produto("Produto H", 25.20m),
                new Produto("Produto I", 19.5m),
                new Produto("Produto J", 10m)
            );
        }
    }
}