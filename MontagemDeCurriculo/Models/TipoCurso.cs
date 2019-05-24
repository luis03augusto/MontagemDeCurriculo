using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Models
{
    public class TipoCurso
    {
        public int TipoCursoId { get; set; }
        public string Tipo { get; set; }
        public ICollection<FormacaoAcademica> FormacoesAcademica { get; set; }
    }
}
