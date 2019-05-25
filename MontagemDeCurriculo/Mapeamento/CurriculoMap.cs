using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class CurriculoMap : IEntityTypeConfiguration<Curriculo>
    {
        public void Configure(EntityTypeBuilder<Curriculo> builder)
        {
            builder.HasKey(x => x.CurriculoId);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Nome)
                .IsUnique();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Curriculos)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.Objetivos)
                .WithOne(x => x.Curriculo)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.FormacoesAcademica)
                .WithOne(x => x.Curriculo)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ExperienciasProfissional)
                .WithOne(x => x.Curriculo)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Idiomas)
                 .WithOne(x => x.Curriculo)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Curriculos");


        }
    }
}
