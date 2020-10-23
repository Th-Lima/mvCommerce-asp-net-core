using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PagarMe;

namespace mvCommerce.Libraries.Manager.Payment.PagarMe
{
    public class ManagementPagarMe
    {
        private IConfiguration _configuration { get; set; }
       
        public ManagementPagarMe(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void GenerateBoleto()
        {
            PagarMeService.DefaultApiKey = _configuration.GetValue<string>("Payment:PagarMe:ApiKey");
            PagarMeService.DefaultEncryptionKey = _configuration.GetValue<string>("Payment:PagarMe:EncryptionKey");

            Transaction transaction = new Transaction();

            transaction.Amount = 2100;
            transaction.Card = new Card
            {
                Id = "card_cj95mc28g0038cy6ewbwtwwx2"
            };

            transaction.Customer = new Customer
            {
                ExternalId = "#3311",
                Name = "Rick",
                Type = CustomerType.Individual,
                Country = "br",
                Email = "rick@morty.com",
                Documents = new[]
                  {
                    new Document{
                     Type = DocumentType.Cpf,
                     Number = "11111111111"
                  },
                     new Document{
                     Type = DocumentType.Cnpj,
                     Number = "83134932000154"
                  }
            },
                PhoneNumbers = new string[]
              {
                "+5511982738291",
                "+5511829378291"
              },
                Birthday = new DateTime(1991, 12, 12).ToString("yyyy-MM-dd")
              };

            transaction.Save();
        }
    }
}
