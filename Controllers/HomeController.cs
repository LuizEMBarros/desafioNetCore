using ConsomeApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsomeApis.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace ConsomeApis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculo()
        {
            return View();
        }

        public IActionResult Calcular(calculaJurosViewModel calculo)
        {
            string uriapi = "https://localhost:44346/api/calculaJuros";

            var retorno = CalculaParcela(uriapi, calculo);
            calculo.ValorFinal = retorno.Result.ToString();
            ViewBag.Resultado = retorno.Result.ToString();

            return View("Calculo",calculo);

        }
        public static async Task<string> CalculaParcela(string URI, calculaJurosViewModel calculo)
        {
            string resultado = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var serializeCalc = JsonConvert.SerializeObject(calculo);
                    var content = new StringContent(serializeCalc, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(URI, content);
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        resultado = readTask.Result;
                    }
                    else
                    {
                        resultado = "";
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro no servidor.Verifique se as APIS estão disponíveis");
            }

            return resultado;
        }

        public IActionResult TaxaJuros()
        {
            string URI = "https://localhost:44319/api/taxaJuros";
            string taxajuros = string.Empty;
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

                        taxajuros = readTask.Result;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro no servidor.Verifique se as APIS estão disponíveis");
            }
            ViewBag.Taxa = taxajuros;
            return View("Privacy");
        }

        public IActionResult ShowCode()
        {
            string URI = "https://localhost:44346/api/showmethecode";
            string showcode = string.Empty;
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

                        showcode = readTask.Result;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro no servidor.Verifique se as APIS estão disponíveis");
            }
            ViewBag.ShowCode = showcode;
            return View("Privacy");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
