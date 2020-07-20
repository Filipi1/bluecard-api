using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using creditcard_api.Data;
using creditcard_api.Models;
using creditcard_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace creditcard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly DataContext _context;

        public TransactionController(IMapper mapper, AuthenticatedUser user, DataContext context) {
            _mapper = mapper;
            _authenticatedUser = user;
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<List<TransactionViewModel>>> Get(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                var transactions = await _context.Transactions.Include(x => x.Category).AsNoTracking()
               .Where(s => s.UserId == id).ToListAsync();
                List<TransactionViewModel> content = _mapper.Map<List<TransactionViewModel>>(transactions);

                return Ok(content.Reverse<TransactionViewModel>());
            }

            return BadRequest(new { message = "Usuário não autenticado" });
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Transaction>> Create([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.Users.Where(u => u.Id == transaction.UserId).FirstOrDefaultAsync();
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                var kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var horaBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, kstZone);

                if (user.CreditLimit > transaction.Value)
                {
                    // APLICA A TRANSAÇÃO E ADICIONA OS VALORES AO BANCO
                    transaction.Parcels = transaction.Parcels == 0 ? 1 : transaction.Parcels;
                    transaction.TotalValue = (float)transaction.Value;
                    transaction.Value = transaction.Parcels > 1 ? (transaction.TotalValue / transaction.Parcels) : transaction.TotalValue;
                    transaction.OperationDate = horaBrasilia;
                    transaction.CategoryId = transaction.CategoryId == 0 ? 1 : transaction.CategoryId;
                    _context.Transactions.Add(transaction);

                    // APLICA O VALOR DA TRANSAÇÃO A FATURA DO CLIENTE
                    user.Invoice += transaction.TotalValue;
                    user.CreditLimit -= user.Invoice;

                    _context.Entry<User>(user).State = EntityState.Modified;
                } else {
                    return BadRequest(new { message = "Limite de crédito excedido." });
                }

                await _context.SaveChangesAsync();

                return Ok(transaction);
            }

            return BadRequest(new { message = "Usuário não encontrado" });
        }
    }
}
