using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class InformacaoLoginMap : IEntityTypeConfiguration<InformacaoLogin>
    {
        public void Configure(EntityTypeBuilder<InformacaoLogin> builder)
        {
            builder.HasKey(x => x.InformacaoId);

            builder.Property(x => x.EnderecoIP)
                .IsRequired();

            builder.Property(x => x.Horario)
                .IsRequired();

            builder.Property(x => x.Data)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.InformacaoLogin)
                .HasForeignKey(x => x.UsuarioId);

            builder.ToTable("InformacoesLogin");

        }
    }
}
