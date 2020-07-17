using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace creditcard_api.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public String Name { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public float Value { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public int CategoryId { get; set; }
        public int Parcels { get; set; }
        public float TotalValue { get; set; }
        public DateTime DataOperacao { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
