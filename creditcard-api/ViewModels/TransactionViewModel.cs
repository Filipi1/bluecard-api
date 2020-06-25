using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace creditcard_api.Models
{
    public class TransactionViewModel
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }
        public Category Category { get; set; }
        public int Parcels { get; set; }
        public String TotalValue { get; set; }
        public String DataOperacao { get; set; }
    }
}
