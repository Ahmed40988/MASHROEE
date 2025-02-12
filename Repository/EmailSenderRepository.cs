using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MASHROEE.IRepository;

namespace MASHROEE.Repository
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
        private readonly string _sendGridApiKey;

        public EmailSenderRepository(IConfiguration configuration)
        {

            _sendGridApiKey = Environment.GetEnvironmentVariable("SendGrid_ApiKey")
                              ?? configuration["SendGrid:ApiKey"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(_sendGridApiKey))
            {
                throw new Exception("SendGrid API Key is missing! Make sure it's set in Environment Variables.");
            }

            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("am7883@fayoum.edu.eg", "MASHROEE");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email: {response.StatusCode}");
            }
        }
    }
}
