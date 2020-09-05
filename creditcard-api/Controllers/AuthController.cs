using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context) {
            _context = context;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Auth([FromBody] Auth model)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.CPF == model.CPF && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = null;

            return new {
                user = user,
                auth = token
            };
        }
    }
}
