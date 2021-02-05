using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace SoTelAPI.Email
{
    public class SendMail
    {
        private SmtpClient client;
        public SendMail()
        {
            this.client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("638fd2fe37296a", "a2b26c53245586"),
                EnableSsl = true
            };
          
        }

        public void NewUser(string callbackUrl)
        {
            client.Send("from@example.com", "to@example.com", "Confirm your account",  callbackUrl );
        }
    }
}
