using Portafolio.Models;
using System.Net.Mail;
using System.Net;

namespace Portafolio.Servicios
{
    public interface IServicioEmail
    {
        Task Enviar(ContactoViewModel contacto);
    }
    public class ServicioEmailSend: IServicioEmail
    {
        private readonly IConfiguration configuration;

        public ServicioEmailSend(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Enviar(ContactoViewModel contacto)
        {
            var apiKey = configuration.GetValue<string>("MAILTRAP_API_KEY");
            var apiPass = configuration.GetValue<string>("MAILTRAP_API_PASS");
            var apiPort = configuration.GetValue<int>("MAILTRAP_API_PORT");
            var apiAddress = configuration.GetValue<string>("MAILTRAP_API_ADDRESS");
            var email = configuration.GetValue<string>("MAILTRAP_FROM");

            var client = new SmtpClient(apiAddress, apiPort)
            {
                Credentials = new NetworkCredential(apiKey, apiPass),
                EnableSsl = true
            };
            var subjec = $"El cliente {contacto.Email} quiere contactarte";
            var body = 
                @$" 
                from: {contacto.Email}
                Soy {contacto.Nombre} y este es mi mensaje:
                {contacto.Mensaje}
                ";
            client.Send(contacto.Email, email, subjec, body);
            System.Console.WriteLine("Sent");
        }
    }
}
