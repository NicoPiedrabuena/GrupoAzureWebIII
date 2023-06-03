using GrupoAzureWebIII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace GrupoAzureWebIII.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly AzureFunctionTuSecreto _AzureFunctionTuSecreto;

      public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _AzureFunctionTuSecreto = new AzureFunctionTuSecreto();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
<<<<<<< HEAD
        public async Task<IActionResult> GoAzure()
        { 
            string result = await _AzureFunctionTuSecreto.CallAzureFunction("hola", "rey", 4);
            if (result == "success")
            {
                return RedirectToAction("Confirmacion");

            }
            else {
                return RedirectToAction("HomeS");
            }
           
=======
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
>>>>>>> 2eb717d8507fead2b6345019e638da3eb4be5dfd
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