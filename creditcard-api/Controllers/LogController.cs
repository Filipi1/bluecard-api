using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using creditcard_api.Data;
using creditcard_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace creditcard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IMapper _mapper;
        

        public LogController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("obter")]
        public async Task<ActionResult<List<TransactionViewModel>>> Get([FromServices]DataContext context)
        {
            var transactions = await context.Transactions.Include(x => x.Category).AsNoTracking().ToListAsync();
            List<TransactionViewModel> content = _mapper.Map<List<TransactionViewModel>>(transactions);

            return Ok(content.Reverse<TransactionViewModel>());
        }

        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult<Transaction>> Create([FromBody]Transaction transaction, [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var horaBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, kstZone);

            if (transaction.Parcels > 1) {
                transaction.TotalValue = transaction.Value;
                transaction.Value = transaction.TotalValue / transaction.Parcels;
                transaction.DataOperacao = horaBrasilia;
            } else {
                transaction.TotalValue = transaction.Value;
                transaction.DataOperacao = horaBrasilia;
            }

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return Ok(transaction);
        }
    }
}
