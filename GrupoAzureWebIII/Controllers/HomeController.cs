using GrupoAzureWebIII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
 

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

      
        public async Task<IActionResult> GoAzure()
        { 
             await _AzureFunctionTuSecreto.CallAzureFunction("hola", "rey", 4);
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