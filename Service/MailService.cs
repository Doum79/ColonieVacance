using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Service
{
    public class MailService
    {
        private readonly ILogger<MailService> logger;
        private readonly IConfiguration config;

        public MailService(ILogger<MailService> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        public async Task<Response> Registration(string email)
        {
            var data = new DataMail
            {
                Email = email,
                UrlDirection = config["ApplicationSettings:ClienUrl"],
            };

            return await SendTemplate(email, config["SendGrid:Templates:Registration"], data);
        }

        private async Task<Response> SendTemplate(string to, string templateId, object dynamicTemplateData)
        {
            var apiKey = config["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(config["SendGrid:From"], config["SendGrid:Name"]));
            msg.AddTo(new EmailAddress(to));
            msg.SetTemplateId(templateId);

            msg.SetTemplateData(dynamicTemplateData);
            var response = await client.SendEmailAsync(msg);
            return response;
        }

        private class DataMail
        {
            [JsonProperty("MAIL")]
            public string Email { get; set; }

            [JsonProperty("PRENOM")]
            public string FirstName { get; set; }

            [JsonProperty("NOM")]
            public string LastName { get; set; }

            [JsonProperty("MOTDEPASSE")]
            public string Password { get; set; }

            [JsonProperty("URLDIRECTION")]
            public string UrlDirection { get; set; }
        }
    }
}
