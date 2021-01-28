using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class UsuarioViewModel
    {
        public int? Codigo { get; set; }
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Informe o Email")]
        [EmailAddress(ErrorMessage = "Inserir um endereço de e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Defina uma Senha")]
        public string Senha { get; set; }
    }
}
