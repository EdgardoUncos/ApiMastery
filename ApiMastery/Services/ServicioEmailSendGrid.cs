using ApiMastery.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ApiMastery.Services
{
    public interface IServicioEmail
    {
        Task Enviar(string _email, string _subject, string _htmlMessage);
    }
    public class ServicioEmailSendGrid: IServicioEmail
    {
        public readonly IConfiguration configuration;
        private readonly SmtpSettings _smtpSettings;

        public ServicioEmailSendGrid(IConfiguration configuration, IOptions<SmtpSettings> smtpSettings)
        {
            this.configuration = configuration;
            _smtpSettings = smtpSettings.Value;
        }

        public async Task Enviar(string _email, string _subject, string _htmlMessage)
        {
            var apiKey = configuration.GetValue<string>("SENDGRID_API_KEY");
            var email = _email;
            var nombre = "Nuevo cliente";

            var cliente = new SendGridClient(apiKey);
            var from = new EmailAddress(email, nombre);
            var subject = _subject;
            var to = new EmailAddress(email, nombre);
            var mensajeTextoPlano = "Por favor verifica tu cuenta";
            var contenidoHtml = _htmlMessage;
            var singleEmail = MailHelper.CreateSingleEmail(from, to, subject, mensajeTextoPlano, contenidoHtml);
            var respuesta = await cliente.SendEmailAsync(singleEmail);
        }
    }
}
