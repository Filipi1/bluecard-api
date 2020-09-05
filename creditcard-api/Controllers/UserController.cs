using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using creditcard_api.Data;
using creditcard_api.Models;
using creditcard_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace creditcard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                model.Role = "User";
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateUser), model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar o usuário" });
            }
        }

        [HttpGet("{uid}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<User>> GetUser(int uid)
        {
            var user = await _context.Users.Where<User>(u => u.Id == uid).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(user);
        }

        [HttpPut("{uid}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<UserViewModel>> UpdateUser(int uid, User model)
        {
            var user = await _context.Users.Where<User>(u => u.Id == uid).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });
            else
                _context.Entry(user).State = EntityState.Detached;

            _context.Entry<User>(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(model);
        }
    }
}
