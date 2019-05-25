using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class ObjetivosMap : IEntityTypeConfiguration<Objetivo>
    {   
        public void Configure(EntityTypeBuilder<Objetivo> builder)
        {
            builder.HasKey(x => x.ObjetivoId);
            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(x => x.Curriculo)
                .WithMany(x => x.Objetivos)
                .HasForeignKey(x => x.CurriculoId);

            builder.ToTable("Objetivos");
        }
    }
}
