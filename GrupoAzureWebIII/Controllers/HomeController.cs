using GrupoAzureWebIII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using GrupoAzureWebIII.ViewModels;
using GrupoAzureWebIII.Services;

namespace GrupoAzureWebIII.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly AzureFunctionTuSecreto _AzureFunctionTuSecreto;

        private readonly TwitterService _twitterService;
        private readonly EmailService _emailService;
        
        private string enviarMailApiKey = "4o7pi4RkG5eeewDF7iVBIMYZw76k7YxBQadnMH_jA9FNAzFujT2_pQ==";
        private string enviarTuitApiKey = "wnTR9oLhe31T1494bixlG0i_RTfrYwVJlYLj7NYLjKBfAzFuPGPOrg==";

        private string enviarMailUrl = "https://connecttoapi.azurewebsites.net/api/SendEmail";
        private string enviarTuitUrl = "https://connecttoapi.azurewebsites.net/api/TweetFunction";



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _AzureFunctionTuSecreto = new AzureFunctionTuSecreto();
            _twitterService = new TwitterService(enviarTuitUrl,enviarTuitApiKey);
            _emailService = new EmailService(enviarMailUrl,enviarMailApiKey);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Confirmacion()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    

    [HttpPost]
    //editar elementos de entrada y a donde dirigirlos dependiendo la opcion ....
    public async Task<IActionResult> EnviarMensaje(FormViewModel form)
    {
        
            if (form.publicarTwitter)
            {
                bool tweetPublicado = await _twitterService.PublicarTweet(form.mensaje,form.apodo,form.user);
                if (tweetPublicado)
                {
                    // Hacer algo después de publicar en Twitter
                    return RedirectToAction("Confirmacion");
                }
                else
                {
                    ViewBag.mensajeError = "No se pudo publicar en Twitter";
                    return View("Index");
                }
            }

            else if (form.publicarEmail)
            {
                bool mailEnviado = await _emailService.EnviarEmail(form.mensaje, form.user);
                if (mailEnviado)
                {
                    // Hacer algo después de publicar en Twitter
                    return RedirectToAction("Confirmacion");
                }
                else
                {
                    ViewBag.mensajeError = "No se pudo enviar el Mail";
                    return View("Index");
                }
            }

        // Si no se seleccionó ninguna opción, puedes mostrar un mensaje de error o redirigir a una página de error
        return View("Error");

    }
   }
}
