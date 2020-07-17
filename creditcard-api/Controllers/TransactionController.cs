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
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<TransactionViewModel>>> Get()
        {
            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(_authenticatedUser.Id);

                var transactions = await _context.Transactions.Include(x => x.Category).AsNoTracking()
               .Where(s => s.UserId == int.Parse(_authenticatedUser.Id)).ToListAsync();
                List<TransactionViewModel> content = _mapper.Map<List<TransactionViewModel>>(transactions);

                return Ok(content.Reverse<TransactionViewModel>());
            }

            return BadRequest("Usuário não autenticado");
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Transaction>> Create([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.User.Where(u => u.Id == transaction.UserId).ToListAsync();
            if (user.Count == 0)
                return BadRequest("Usuário não encontrado");

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                var kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var horaBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, kstZone);

                transaction.Parcels = transaction.Parcels == 0 ? 1 : transaction.Parcels;
                transaction.TotalValue = (float) transaction.Value;
                transaction.Value = transaction.Parcels > 1 ? (transaction.TotalValue / transaction.Parcels) : transaction.TotalValue;
                transaction.OperationDate = horaBrasilia;
                transaction.CategoryId = transaction.CategoryId == 0 ? 1 : transaction.CategoryId;

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return Ok(transaction);
            }

            return BadRequest("Usuário não autenticado");
        }
    }
}
