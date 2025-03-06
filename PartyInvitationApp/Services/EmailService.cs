////DRASTI PATEL
////PROBLEM ANALYSIS 2
////MARCH 05, 2025 

//using System;
//using System.Net;
//using System.Net.Mail;

//namespace PartyInvitationApp.Services  
//{
//    // Static class responsible for sending email invitations for the Party Invitation App.
//    public static class EmailService  
//    {
//        public static void SendInvitation(string toEmail, string guestName, string partyName, string partyLocation, DateTime partyDate, int invitationId)
//        {
//            //Generates a unique response link for the invitation
//            var responseLink = $"http://localhost:5002/Invitation/Respond/{invitationId}";

//            //Sets up the sender email address and display name
//            var fromAddress = new MailAddress("drasti.patel2402@gmail.com", "Party Manager App");

//            // Creates the recipient email address object
//            var toAddress = new MailAddress(toEmail, guestName);

//            //Secure application password 
//            const string fromPassword = "qpihsbzjwwnynoxq";

//            // Configures the SMTP client to send emails using Gmail
//            var smtp = new SmtpClient
//            {
//                Host = "smtp.gmail.com",    // Gmail SMTP server address
//                Port = 587,                 // TLS encryption port for secure connection
//                EnableSsl = true,           // Enables secure connection via SSL
//                DeliveryMethod = SmtpDeliveryMethod.Network,       // Sends emails over the network
//                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),       // Authenticates the email sender
//                Timeout = 20000              // Timeout in milliseconds for sending the email
//            };

//            //Constructs the email message with HTML formatting
//            using (var message = new MailMessage(fromAddress, toAddress)
//            {
//                Subject = $"You're Invited to {partyName}!",      // Email subject
//                Body = $@"
//                <h1>Hello {guestName},</h1>
//                <p>You have been invited to <strong>{partyName}</strong> at <strong>{partyLocation}</strong> on <strong>{partyDate:MM/dd/yyyy}</strong>.</p>
//                <p>We would be thrilled to have you! Please let us know your availability by clicking the link below:</p>
//                <p><a href='{responseLink}'>Respond to Invitation</a></p>
//                <p>Sincerely,<br>The Party Manager App</p>",
//                IsBodyHtml = true
//            })

//            {
//                smtp.Send(message);     //Sends the email invitation
//            }
//        }
//    }
//}


using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace PartyInvitationApp.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendInvitation(string toEmail, string guestName, string partyName, string partyLocation, DateTime partyDate, int invitationId)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            var responseLink = $"http://localhost:5002/Invitation/Respond/{invitationId}";

            var fromAddress = new MailAddress(senderEmail, "Party Manager App");
            var toAddress = new MailAddress(toEmail, guestName);

            var smtp = new SmtpClient
            {
                Host = smtpServer,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = $"You're Invited to {partyName}!",
                Body = $@"
                <h1>Hello {guestName},</h1>
                <p>You have been invited to <strong>{partyName}</strong> at <strong>{partyLocation}</strong> on <strong>{partyDate:MM/dd/yyyy}</strong>.</p>
                <p>We would be thrilled to have you! Please let us know your availability by clicking the link below:</p>
                <p><a href='{responseLink}'>Respond to Invitation</a></p>
                <p>Sincerely,<br>The Party Manager App</p>",
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
