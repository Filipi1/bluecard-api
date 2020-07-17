using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace creditcard_api.Models
{
    public class AutoMapping:Profile
    {
        private string ToCurrency(double? value) {
            return value?.ToString("C");
        }

        public AutoMapping()
        {
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(a => a.TotalValue, opt => opt.MapFrom(b => b.TotalValue.ToString("C")))
                .ForMember(a => a.Value, opt => opt.MapFrom(b => ToCurrency(b.Value)))
                .ForMember(a => a.OperationDate, opt => opt.MapFrom(b => $"{b.OperationDate.ToShortDateString()} às {b.OperationDate.ToShortTimeString()}"));
        }
    }
}
