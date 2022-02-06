using Microsoft.AspNetCore.Mvc;

namespace ChatWebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NoAccess()
        {
            return View();
        }
    }
}