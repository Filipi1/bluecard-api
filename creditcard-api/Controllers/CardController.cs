﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace creditcard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpPut]
        [Route("obter")]
        public async Task<ActionResult> Get()
        {
            return Ok("Cartão Bloqueado");
        }
    }
}
