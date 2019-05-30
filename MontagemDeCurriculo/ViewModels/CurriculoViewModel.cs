using MontagemDeCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.ViewModels
{
    public class CurriculoViewModel
    {
        public IEnumerable<Objetivo> Objetivos { get; set; }
        public IEnumerable<FormacaoAcademica> FormacaoAcademicas { get; set; }
        public IEnumerable<ExperienciaProfissional> ExperienciaProfissionals { get; set; }
        public IEnumerable<Idioma> Idiomas { get; set; }        
    }
}
