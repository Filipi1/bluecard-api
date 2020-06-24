using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using creditcard_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace creditcard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpGet]
        [Route("obter")]
        public async Task<ActionResult<List<TransactionModel>>> Get()
        {
            List<TransactionModel> models = new List<TransactionModel>();

            TransactionModel model = new TransactionModel();
            model.Name = "Amazon Prime";
            model.Value = "R$ 9,99";
            model.Category = "Eletrônicos";
            model.Parcels = 1;
            model.TotalValue = "R$ 20,99";
            model.DataOperacao = "20/11/2013 às 14:23";


            TransactionModel modeld = new TransactionModel();
            modeld.Name = "Pagseguro Fran*";
            modeld.Value = "R$ 22,99";
            modeld.Category = "Educação";
            modeld.Parcels = 5;
            modeld.TotalValue = "R$ 114,95";
            modeld.DataOperacao = "23/06/2013 às 20:23";

            models.Add(model);
            models.Add(modeld);

            return models;
        }
    }
}
