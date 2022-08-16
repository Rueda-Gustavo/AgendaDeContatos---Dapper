using AgendaDeContatos.Infra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaDeContatos.Infra.EntityConfiguration
{
    internal class CategoriaEntityTypeConfiguration : IEntityTypeConfiguration<Categoria>
    {
        
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable($"{nameof(Categoria)}s");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Descricao)
            //.HasColumnName("Descricao") //Não é necessário             
            .IsUnicode(false)
            .HasMaxLength(50)
            .IsRequired();
        
            builder
            .HasMany(cont => cont.Contatos)
            .WithOne(cat => cat.Categoria)
            .HasForeignKey(cont => cont.IdCategoria);

            builder.HasData(new Categoria { Id = 1, Descricao = "Família" },
                            new Categoria { Id = 2, Descricao = "Trabalho" }, 
                            new Categoria { Id = 3, Descricao = "Escola" });
        }
    }
}
