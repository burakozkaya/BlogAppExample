using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppExample.BLL.Abstract
{
    public interface IEmailService
    {
       void SendEmail(string reciverEMailAdress,string subject,string mailBody);
    }
}
