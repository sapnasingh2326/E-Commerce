using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace ECommerceWeb.Pages
{
    public class SendSMSModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public SendSMSModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public string EmailAddress { get; set; }

        [BindProperty]
        public string EmailSubject { get; set; }

        [BindProperty]
        public string EmailMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            bool smsSuccess = false;
            bool emailSuccess = false;

            // Sending SMS Logic
            try
            {
                string accountSid = _configuration["TwilioSettings:AccountSid"];
                string authToken = _configuration["TwilioSettings:AuthToken"];
                string twilioPhoneNumber = _configuration["TwilioSettings:PhoneNumber"];

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(PhoneNumber);
                var from = new PhoneNumber(twilioPhoneNumber);

                // Send the SMS
                var smsMessage = MessageResource.Create(
                    body: Message,
                    from: from,
                    to: to
                );

                smsSuccess = true;
                TempData["Success"] = "SMS sent successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error sending SMS: {ex.Message}";
            }

            // Sending Email Logic
            try
            {
                string smtpServer = _configuration["SMTPSettings:Server"];
                int smtpPort = int.Parse(_configuration["SMTPSettings:Port"]);
                string smtpUsername = _configuration["SMTPSettings:Username"];
                string smtpPassword = _configuration["SMTPSettings:Password"];
                string senderEmail = _configuration["SMTPSettings:SenderEmail"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = EmailSubject,
                        Body = EmailMessage,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(EmailAddress);

                    client.Send(mailMessage);
                    emailSuccess = true;
                    TempData["Success"] += " Email sent successfully!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] += $" Error sending email: {ex.Message}";
            }

            return RedirectToPage("/SendSMS");
        }
    }
}
