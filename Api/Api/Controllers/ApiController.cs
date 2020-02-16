using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        [HttpGet("[action]")]
        public ActionResult<string> TaxaJuros()
        {
            return "0,01";
        }
    }
}