using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Models
{
    public class Idioma
    {
        public int IdiomaId { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        public string Nivel { get; set; }
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
