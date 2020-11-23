using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BookBook.Models.Utils
{
    public class MailSender
    {
        public static void SendEmail(string emailReceiver, string nameReceiver, string subject, string message,
            string[] fileattach)
        {
            var senderEmail = new MailAddress("quando.2299@gmail.com", "Book Book");
            var password = "Quando2299@!";

            //var senderEmail = new MailAddress("hello@yoot.vn", "YOOT Admin");
            //var password = "Yoot@2019";

            var receiverEmail = new MailAddress(emailReceiver, nameReceiver);

            //Google
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                //Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            var mess = new MailMessage(senderEmail, receiverEmail);
            mess.Subject = subject;
            mess.Body = message;
            mess.IsBodyHtml = true;

            if (fileattach != null)
            {
                foreach (var itemFile in fileattach)
                {
                    mess.Attachments.Add(new Attachment(itemFile));
                }
            }

            smtp.Send(mess);
        }
    }
}