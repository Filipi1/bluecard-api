using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace creditcard_api.Models
{
    public class TransactionModel
    {
        public String Name { get; set; }
        public String Value { get; set; }
        public String Category { get; set; }
        public int Parcels { get; set; }
        public String TotalValue { get; set; }
        public String DataOperacao { get; set; }
    }
}
