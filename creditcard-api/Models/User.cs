using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace creditcard_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Esse campo deve ter entre 3 e 20 Caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve ter entre 3 e 20 Caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }

        public double Balance { get; set; }

        public double Invoice { get; set; }

        public bool OpenInvoice { get; set; }

        public double CreditLimit { get; set; }

        public int MaxCreditLimit { get; set; }

        public string Role { get; set; }

        public string ImageProfile { get; set; }
    }
}
