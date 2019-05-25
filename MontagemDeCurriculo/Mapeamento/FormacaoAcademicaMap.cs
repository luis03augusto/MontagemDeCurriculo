using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class FormacaoAcademicaMap : IEntityTypeConfiguration<FormacaoAcademica>
    {
        public void Configure(EntityTypeBuilder<FormacaoAcademica> builder)
        {
            builder.HasKey(x => x.FormacaoAcademicaId);

            builder.Property(x => x.Instituicao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.AnoInicio)
                .IsRequired();

            builder.Property(x => x.AnoFim)
                .IsRequired();

            builder.Property(x => x.NomeCurso)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.TipoCurso)
                .WithMany(x => x.FormacoesAcademica)
                .HasForeignKey(x => x.TipoCursoId);

            builder.HasOne(x => x.Curriculo)
                .WithMany(x => x.FormacoesAcademica)
                .HasForeignKey(x => x.CurriculoId);

            builder.ToTable("FormacoesAcademicas");
        }
    }
}
