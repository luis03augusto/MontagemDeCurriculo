using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class IdiomaMap : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> builder)
        {
            builder.HasKey(x => x.IdiomaId);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Nome)
                .IsUnique();

            builder.Property(x => x.Nivel)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.Curriculo)
                .WithMany(x => x.Idiomas)
                .HasForeignKey(x => x.CurriculoId);

            builder.ToTable("Idiomas");
        }
    }
}
