using System;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SendGrid;
using Newtonsoft.Json;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

 

public static class EnviarEmail

{

    [FunctionName("SendEmail")]

    public static async Task<IActionResult> RunAsync(

        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,

        ILogger log)

    {

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        dynamic data = JsonConvert.DeserializeObject(requestBody);



        var senderEmail = "pweb3grupoazure@gmail.com";

        var senderName = "Nombre del remitente"; // Puedes personalizar este valor

        var recipientEmail = "rodrigo.pereira@outlook.es";

        var subject = "descargo anonimo";

        var content = "esta es una prueba";

        var sendGridApiKey = "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";



        var msg = new SendGridMessage()

        {

            From = new EmailAddress(senderEmail, senderName),

            Subject = subject,

            PlainTextContent = content,

            HtmlContent = content

        };

        msg.AddTo(new EmailAddress(recipientEmail));




        var client = new SendGridClient(sendGridApiKey);

        var response = await client.SendEmailAsync(msg);



        if (response.IsSuccessStatusCode)

        {

            log.LogInformation("Email sent successfully");

            return new OkResult();

        }

        else

        {

            log.LogError($"Failed to send email: {response.StatusCode}");

            return new StatusCodeResult(500);

        }

    }

}
