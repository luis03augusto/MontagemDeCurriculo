using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Models
{
    public class Objetivo
    {
        public int ObjetivoId { get; set; }
        [Required(ErrorMessage = "Descrição obritória")]
        [StringLength(1000, ErrorMessage ="{0} muito longa")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
