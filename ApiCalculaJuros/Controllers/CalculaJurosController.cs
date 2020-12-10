using ApiCalculaJuros.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiCalculaJuros.Controllers
{
    //[Route("api/calculaJuros")]
    public class CalculaJurosController : ControllerBase
    {
        private static double dResultado = 0.0;
        private static string sResultado = string.Empty;
        [Route("api/calculaJuros")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Post([FromBody] calculaJuros calculo)
        {
            var Calculo = new calculaJuros();
            Calculo.ValorInicial = calculo.ValorInicial;
            Calculo.Meses = calculo.Meses;
            //Consulta a taxa na api 1 - "/taxaJuros" 
            Calculo.ValorTaxa = TaxaJuros();
            //expressão do calculo - OK
            //dResultado = Calculo.ValorInicial * (1+Calculo.ValorTaxa);
            dResultado = Math.Pow(1+Calculo.ValorTaxa, Calculo.Meses);
            dResultado = (dResultado * Calculo.ValorInicial);
            sResultado = dResultado.ToString("N2");
            return sResultado;
        }
        //Procedimento consome api e retorna taxa de juros
        public double TaxaJuros()
        {
            double taxajuros = 0;
            string URI = "https://localhost:44319/api/taxaJuros";
            string taxaret = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URI);
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        taxaret = readTask.Result;
                        taxaret = taxaret.Replace('.', ',');
                        taxajuros = Convert.ToDouble(taxaret);
                   }
                }
            }
            catch (Exception)
            {
                taxajuros = 0;
                throw new Exception("Erro no servidor.Verifique se as APIS estão disponíveis");
            }
            return taxajuros;
        }
    }
}
