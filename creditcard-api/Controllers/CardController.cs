using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Threading.Tasks;
using creditcard_api.Data;
using creditcard_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace creditcard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly DataContext _context;

        public CardController(DataContext context)
        {
            _context = context;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            switch(id)
            {
                case 0: return Ok("Cartão Desbloqueado");
                case 1: return Ok("Cartão Bloqueado");
                default: return Ok("Nenhuma ação para esse id");
            }
        }

        [HttpGet]
        [Route("acoes")]
        public async Task<ActionResult> ObterAcoes()
        {
            var listActions = await _context.Actions.ToListAsync();
            return Ok(listActions);
        }
    }
}
