using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace creditcard_api.Models
{
    public class Auth
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Password { get; set; }
    }
}
