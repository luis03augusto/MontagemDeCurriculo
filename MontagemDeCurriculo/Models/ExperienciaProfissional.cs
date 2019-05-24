using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Models
{
    public class ExperienciaProfissional
    {
        public int ExperienciaProfissionalId { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        public string NomeEmpresa { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Range(1920, 2019, ErrorMessage = "Ano inválido")]
        public int AnoInicio { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Range(1920, 2019, ErrorMessage = "Ano inválido")]
        public int AnoFim { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [DataType(DataType.MultilineText)]
        public string DescricaoAtividade  { get; set; }
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
