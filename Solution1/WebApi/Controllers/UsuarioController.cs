using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
