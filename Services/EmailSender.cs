using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore; // Required for async EF Core methods
using ECommerceWeb.Model;

namespace ECommerceWeb.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _config;
        private readonly DemoProjectContext _dbContext;

        public EmailSender(IConfiguration config, DemoProjectContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves SMTP configuration from the database asynchronously.
        /// </summary>
        /// <returns>A Task containing the Smtpconfig object or null if not found.</returns>
        public async Task<Smtpconfig> GetSmtpconfigAsync()
        {
            return await _dbContext.Smtpconfigs.FirstOrDefaultAsync(x => x.IsDeleted==false);
        }

        /// <summary>
        /// Retrieves email template from the database asynchronously.
        /// </summary>
        /// <returns>A Task containing the EmailTemp object or null if not found.</returns>
        public async Task<EmailTemp> GetEmailTemplateAsync()
        {
            return await _dbContext.EmailTemps.FirstOrDefaultAsync(x => x.IsDeleted==false);
        }

        /// <summary>
        /// Sends a registration email asynchronously with HTML content.
        /// </summary>
        /// <param name="toEmail">Recipient's email address.</param>
        /// <param name="customerName">Name of the customer to personalize the email.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SendRegistrationEmailAsync(string toEmail, string customerName)
        {
            var emailTemplate = await GetEmailTemplateAsync();
            var smtpConfig = await GetSmtpconfigAsync();

            if (smtpConfig == null || emailTemplate == null)
            {
                throw new InvalidOperationException("SMTP configuration or email template is missing.");
            }

            // Replace placeholder with the customer's name in the email template content.
            var emailBody = emailTemplate.EmailContent.Replace("{Name}", customerName);

            using (var client = new SmtpClient(smtpConfig.Host, int.Parse(smtpConfig.Port.ToString())))
            {
                client.Credentials = new NetworkCredential(smtpConfig.SmtpUser, smtpConfig.SmtpPassword);
                client.EnableSsl = smtpConfig.IsEnableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpConfig.SmtpUser, smtpConfig.SmtpUser),
                    Subject = emailTemplate.Subject,
                    Body = emailBody,
                    IsBodyHtml = true // Ensures emailBody is treated as HTML
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
 