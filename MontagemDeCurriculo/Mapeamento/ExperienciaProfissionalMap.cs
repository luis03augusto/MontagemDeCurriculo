using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class ExperienciaProfissionalMap : IEntityTypeConfiguration<ExperienciaProfissional>
    {
        public void Configure(EntityTypeBuilder<ExperienciaProfissional> builder)
        {
            builder.HasKey(e => e.ExperienciaProfissionalId);

            builder.Property(x => x.NomeEmpresa)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Cargo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.AnoInicio)
                .IsRequired();

            builder.Property(x => x.AnoFim)
                .IsRequired();

            builder.Property(x => x.DescricaoAtividade)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(x => x.Curriculo)
                .WithMany(x => x.ExperienciasProfissional)
                .HasForeignKey(x => x.CurriculoId);

            builder.ToTable("ExperienciasProfissionais");

        }
    }
}
