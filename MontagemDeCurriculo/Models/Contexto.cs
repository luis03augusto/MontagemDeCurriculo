using Microsoft.EntityFrameworkCore;
using MontagemDeCurriculo.Mapeamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Models
{
    public class Contexto : DbContext
    {

        public DbSet<Curriculo> Curriculos { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<TipoCurso> TiposCursos { get; set; }
        public DbSet<FormacaoAcademica> FormacoesAcademicas{ get; set; }
        public DbSet<Objetivo> Objetivos{ get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<InformacaoLogin> InformacaoLogins { get; set; }
        public DbSet<ExperienciaProfissional> ExperienciaProfissionais { get; set; }

        public Contexto(DbContextOptions<Contexto> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurriculoMap());
            modelBuilder.ApplyConfiguration(new ExperienciaProfissionalMap());
            modelBuilder.ApplyConfiguration(new IdiomaMap());
            modelBuilder.ApplyConfiguration(new TipoCursoMap());
            modelBuilder.ApplyConfiguration(new InformacaoLoginMap());
            modelBuilder.ApplyConfiguration(new ObjetivosMap());
            modelBuilder.ApplyConfiguration(new UsuariosMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
