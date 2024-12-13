using Eticaret.Core.Entities;
using System.Net;
using System.Net.Mail;
namespace Kursprojesi.WebUI.Ultis
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadi.com" ,587);
            smtpClient.Credentials = new NetworkCredential("info@yilmazkasa.com","mailşifre");
            smtpClient.EnableSsl = true;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@yilmazkasa.com");
            message.To.Add("info@kasa.com");
            message.Subject = "Siteden Mesaj Geldi";
            message.Body = $"İsim:{ contact.Name} - Telefon: {contact.Phone}- Email: {contact.Email} ";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
