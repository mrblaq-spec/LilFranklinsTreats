using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;

namespace LilFranklinsTreats.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // begin try.
            try
            {
                //From Address    
                string FromAddress = SD.EmailPrefix + "@gmail.com";
                string FromAdressTitle = "Larry Marshall";

                //To Address    
                string ToAddress = email;
                string ToAdressTitle = "Welcome to Little Franklin's";
                string Subject = subject;
                string BodyContent = htmlMessage;

                //Smtp Server    
                string SmtpServer = "smtp.gmail.com";
                //Smtp Port Number    
                int SmtpPortNumber = 587;

                // create the email message to send from function parameters
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (FromAdressTitle,
                                         FromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (ToAdressTitle,
                                         ToAddress
                                         ));
                mimeMessage.Subject = Subject;

                // make sure body is in html format.
                mimeMessage.Body = new TextPart("html")
                {
                    Text = BodyContent
                };

                // using statement to initialize new smtp
                using var client = new SmtpClient();
                client.Connect(SmtpServer, SmtpPortNumber, false);
                client.Authenticate(FromAddress, SD.EmailSuffix);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            //end try.
            // begin catch.
            catch (Exception)
            {
                throw;
            }
        }
    }
}
