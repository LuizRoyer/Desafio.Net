using Api2.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api2.Controllers
{
    [Route("api/[controller]")]
    public class Api2Controller : Controller
    {          
        [HttpGet("[action]")]
        public string CalcularJuros(string valorInicial, string meses)
        {
            return new CalculaService().CalcularTaxa(valorInicial, meses);
        }
    }
}