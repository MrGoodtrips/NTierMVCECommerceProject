using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class MailService
    {
        public static void Send(string reciever, string password = "flukvcbvzyawbjnv", string body = "Test mesajıdır", string subject = "Email testi", string sender = "mrgoodtripstest@gmail.com")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress recieverEmail = new MailAddress(reciever);

            //EMail işlemleri SMTP'ye göre yapılır...
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            using(MailMessage message = new MailMessage(senderEmail, recieverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
