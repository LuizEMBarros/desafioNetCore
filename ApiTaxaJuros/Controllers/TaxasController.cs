using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaxaJuros.Controllers
{

    [Route("api/taxaJuros")]
    public class TaxasController : ControllerBase
    {
        private static readonly double taxaPadrao = 0.01;
        // GET: api/<TaxasController>
        [HttpGet]
        public double Get()
        {
            return taxaPadrao;
        }
     
    }
}
