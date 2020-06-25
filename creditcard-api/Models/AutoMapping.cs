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
        public AutoMapping()
        {
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(a => a.TotalValue, opt => opt.MapFrom(b => b.TotalValue.ToString("C")))
                .ForMember(a => a.Value, opt => opt.MapFrom(b => b.Value.ToString("C")))
                .ForMember(a => a.DataOperacao, opt => opt.MapFrom(b => $"{b.DataOperacao.ToShortDateString()} às {b.DataOperacao.ToShortTimeString()}"));


        }
    }
}
