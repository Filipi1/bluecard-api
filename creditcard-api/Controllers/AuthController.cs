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
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Auth([FromServices] DataContext dataContext, [FromBody] Auth model)
        {
            var user = await dataContext.User
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = null;

            return new {
                user = user,
                token = token
            };
        }
    }
}
