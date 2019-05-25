using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemDeCurriculo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
