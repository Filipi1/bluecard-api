using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        [Route("obter")]
        public async Task<ActionResult<List<TransactionViewModel>>> Get([FromServices]DataContext context)
        {
            var transactions = await context.Transactions.Include(x => x.Category).AsNoTracking().ToListAsync();

            TransactionViewModel transactionsVModel = new TransactionViewModel();
         

            return Ok(transactions);
        }

        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult<Transaction>> Create([FromBody]Transaction transaction, [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (transaction.Parcels > 1) {
                transaction.TotalValue = transaction.Value;
                transaction.Value = transaction.TotalValue / transaction.Parcels;
                transaction.DataOperacao = DateTime.UtcNow;
            } else {
                transaction.TotalValue = transaction.Value;
                transaction.DataOperacao = DateTime.UtcNow;
            }

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return Ok(transaction);
        }
    }
}
