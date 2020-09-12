using Microsoft.Extensions.Configuration;
using mvCommerce.Models;
using System.Net;
using System.Net.Mail;

namespace mvCommerce.Libraries.Email
{
    public class SendEmail
    {
        private SmtpClient _smtp;
        public IConfiguration _configuration;
        
        public SendEmail(SmtpClient smtp, IConfiguration configuration) 
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public void SendContactPerEmail(Contact contact)
        {
            string bodyMsg = string.Format("<h3>Contato mvCommerce</h3>" + 
                "<b>Nome: </b> {0}<br />" + 
                "<b>E-mail: </b> {1}<br />" + 
                "<b>Texto: </b> {2}<br />",
                contact.Name, 
                contact.Email, 
                contact.Text
             );

            /*
             * MailMessage -> Construct the message
             */
            MailMessage message = new MailMessage();
            message.From = new MailAddress(contact.Email);
            message.To.Add(_configuration.GetValue<string>("Email:Username"));
            message.Subject = "Contato - mvCommerce -" + "Email: " + contact.Email;
            message.Body = bodyMsg;
            message.IsBodyHtml = true;

            //Send message per SMTP
            _smtp.Send(message);
        }

        public void SendPasswordPerEmail(Collaborator collaborator)
        {
            string bodyMsg = string.Format($"<h3>Colaborador mvCommerce</h3>" +
                $"Olá {collaborator.Name}, " +
                $" sua senha é: <h3>{collaborator.Password}</h3>" +
                $"Seja muito bem vindo a nossa empresa! :)");

            /*
             * MailMessage -> Construct the message
             */
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            message.To.Add(collaborator.Email);
            message.Subject = "Geração da senha - mvCommerce - " + "Senha do Colaborador: " + collaborator.Name;
            message.Body = bodyMsg;
            message.IsBodyHtml = true;

            //Send message per SMTP
            _smtp.Send(message);
        }
    }
}
