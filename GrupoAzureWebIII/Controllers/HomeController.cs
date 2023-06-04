using GrupoAzureWebIII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    

    [HttpPost]
    //editar elementos de entrada y a donde dirigirlos dependiendo la opcion ....
    public async Task<IActionResult> EnviarMensaje(string mensaje,
                                                            bool publicarTwitter,
                                                            bool enviarEmail,
                                                            string user)


    {
        string enviarMailApiKey = "4o7pi4RkG5eeewDF7iVBIMYZw76k7YxBQadnMH_jA9FNAzFujT2_pQ==";
        string enviarTuitApiKey = "wnTR9oLhe31T1494bixlG0i_RTfrYwVJlYLj7NYLjKBfAzFuPGPOrg==";


            if (publicarTwitter)
        {
            // Ejecutar lógica para publicar en Twitter utilizando la biblioteca adecuada
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Crea el contenido del cuerpo (body) de la solicitud
                    string requestBody = $"{{\"message\":\"{mensaje}\",\"name\":\"{user}\"}}";
                        // Agrega los encabezados (headers) de la solicitud
                        client.DefaultRequestHeaders.Add("x-functions-key", enviarTuitApiKey);
                    // Realiza una solicitud POST a la API
                    HttpResponseMessage response = await client.PostAsync("https://connecttoapi.azurewebsites.net/api/TweetFunction",
                                                                    new StringContent(requestBody, Encoding.UTF8, "application/json"));
                    // Verifica el código de estado de la respuesta
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Se realizo el tuit correctamente");
                        // Lee el contenido de la respuesta como una cadena JSON
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Procesa la respuesta JSON según sea necesario
                        Console.WriteLine(jsonResponse);
                        return RedirectToAction("Confirmacion");
                    }



                    //HABRIA QUE GUARDAR EL TWIT SI SE MANDA DE MANERA CORRECTA



                    else
                    {
                        // Si la respuesta no es exitosa, muestra el código de estado
                        Console.WriteLine($"Error en la solicitud. Código de estado: {response.StatusCode}");
                        
                        ViewBag.mensajeError = "Algo salió mal";
                        return View("Index");
                    }
                }
                catch (Exception ex)
                {
                    // Captura cualquier excepción ocurrida durante la solicitud
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }




        else if (enviarEmail)
        { // Preparar los datos para enviar en la solicitud HTTP



            // Crea el contenido del cuerpo (body) de la solicitud
            string requestBody = $"{{\"message\":\"{mensaje}\",\"destinatario\":\"{user}\"}}";




            // Crear una instancia de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Agrega los encabezados (headers) de la solicitud
                client.DefaultRequestHeaders.Add("x-functions-key", "");



                // Hacer la solicitud HTTP al endpoint de la función "SendEmail"
                HttpResponseMessage response = await client.PostAsync("https://connecttoapi.azurewebsites.net/api/SendEmail",
                                                    new StringContent(requestBody, Encoding.UTF8, "application/json"));



                // Verificar el estado de la respuesta HTTP
                if (response.IsSuccessStatusCode)
                {
                    // Redirigir a una acción específica después de enviar el correo electrónico
                    return RedirectToAction("CorreoElectronicoEnviado");



                    //HABRIA QUE GUARDAR EL MAIL SI SE MANDA DE MANERA CORRECTA





                }
                else
                {
                    // Si la respuesta no es exitosa, muestra el código de estado
                    Console.WriteLine($"Error en la solicitud. Código de estado: {response.StatusCode}");
                }



            }




        }



        // Si no se seleccionó ninguna opción, puedes mostrar un mensaje de error o redirigir a una página de error
        return View("Error");



    }
   }
}
