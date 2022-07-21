using Microsoft.AspNetCore.Mvc;

namespace ProsperityBank.Controllers
{
    public class ErrorController : Controller
    {

        [HttpGet("/Error/{errorCode}")]
        public IActionResult Index(int errorCode)
        {
            return View(errorCode);
        }
    }
}
