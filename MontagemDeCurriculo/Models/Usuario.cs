using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [EmailAddress(ErrorMessage ="Email Inválido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]        
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public ICollection<FormacaoAcademica> FormacaoAcademicas { get; set; }
        public ICollection<Curriculo> Curriculos { get; set; }

    }
}
