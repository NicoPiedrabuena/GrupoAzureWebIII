using GrupoAzureWebIII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GrupoAzureWebIII.Controllers
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

        [HttpPost]
        public IActionResult Index(string nombre, string apellido, string mensaje,char preferencia)
        {
            // Aquí puedes procesar los datos enviados y realizar cualquier acción necesaria
            if (preferencia.Equals('T')) {
                //aca se llama al servivio de TWITTER 


            }
            else if (preferencia.Equals('M'))
            {
                // aca se llama al servicio de MAIL 


            }
            // Por ejemplo, puedes guardar los datos en una base de datos o enviar un correo electrónico

            // Después de procesar los datos, puedes redirigir a una página de confirmación
            return RedirectToAction("Confirmacion");
        }


        public IActionResult Confirmacion()
        {
            return View();
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