using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalculaJuros.Controllers
{
    [Route("api/showmethecode")]
    public class ShowCodeController : ControllerBase
    {
        private static readonly string  showCode = "https://github.com/LuizEMBarros/desafioNetCore";
        // GET: api/<TaxasController>
        [HttpGet]
        public string Get()
        {
            return showCode;
        }

    }
}
