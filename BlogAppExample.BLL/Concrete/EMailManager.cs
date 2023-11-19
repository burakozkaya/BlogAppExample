using BlogAppExample.BLL.Abstract;
using BlogAppExample.DTO.EMailConfigs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppExample.BLL.Concrete
{
    internal class EMailManager : IEmailService
    {
        private readonly EMailOption _option;

        public EMailManager(IOptions<EMailOption> option)
        {
            _option = option.Value;
        }

        public void SendEmail(string reciverEMailAdress, string subject, string mailBody)
        {
            try
            {
                var smtpClient = new SmtpClient();
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = smtpClient.DeliveryMethod;
                smtpClient.UseDefaultCredentials = false;



                smtpClient.Host = _option.ServiceEmailOption.Host;
                smtpClient.Port = _option.ServiceEmailOption.Port;
                smtpClient.Credentials = new NetworkCredential(_option.ServiceEmailOption.Email, _option.Password);

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_option.ServiceEmailOption.Email);


                mailMessage.To.Add(reciverEMailAdress);
                mailMessage.Subject = subject;
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;

                smtpClient.Send(mailMessage);
                Console.WriteLine("mail başarılı");

            }
            catch (Exception e)
            {

                Console.WriteLine("hata");
            }
            finally { Console.WriteLine("işlem tamamlandı"); }
        }
    }
}
