using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Mapeamento
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.UsuarioId);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Curriculos)
                .WithOne(x => x.Usuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.InformacaoLogin)
                .WithOne(x => x.Usuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Usuarios");
        }
    }
}
