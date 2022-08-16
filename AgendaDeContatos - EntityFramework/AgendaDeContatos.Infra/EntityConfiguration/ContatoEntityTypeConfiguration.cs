using AgendaDeContatos.Infra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDeContatos.Infra.EntityConfiguration
{
    internal class ContatoEntityTypeConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable($"{nameof(Contato)}s");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Celular)
            .IsUnicode(false)
            .HasMaxLength(20);

            builder.Property(p => p.Telefone)
            .IsUnicode(false)
            .HasMaxLength(20);

            builder.Property(p => p.Nome)
            .IsUnicode(false)
            .HasMaxLength(50);

            builder.Property(p => p.Endereco)
            .IsUnicode(false)
            .HasMaxLength(50);

            builder.Property(p => p.Profissao)
            .IsUnicode(false)
            .HasMaxLength(20);

            builder.Property(p => p.NumeroCasa)
            .IsUnicode(false)
            .HasMaxLength(20);

            builder.Property(p => p.Email)
            .IsUnicode(false)
            .HasMaxLength(50);

            builder.Ignore(p => p.Descricao);
            builder.Ignore(p => p.DescricaoCategoria);

            builder.Property(p => p.DataCadastro)
            .HasColumnType("datetime2"); //Não é necessário

        }
    }
}
