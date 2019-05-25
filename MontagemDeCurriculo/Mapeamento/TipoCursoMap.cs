using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class TipoCursoMap : IEntityTypeConfiguration<TipoCurso>
    {
        public void Configure(EntityTypeBuilder<TipoCurso> builder)
        {
            builder.HasKey(x => x.TipoCursoId);

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.HasIndex(x => x.Tipo).IsUnique();

            builder.HasMany(x => x.FormacoesAcademica)
                .WithOne(x => x.TipoCurso)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
