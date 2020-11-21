using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using mvCommerce.Libraries.Login;
using mvCommerce.Libraries.Text;
using mvCommerce.Models;
using PagarMe;

namespace mvCommerce.Libraries.Manager.Payment.PagarMe
{
    public class ManagementPagarMe
    {
        private IConfiguration _configuration;
        private ClientLogin _clientLogin;

        public ManagementPagarMe(IConfiguration configuration, ClientLogin clientLogin)
        {
            _configuration = configuration;
            _clientLogin = clientLogin;
            
        }

        public object GenerateBoleto(decimal value)
        {
            try
            {
                Client client = _clientLogin.GetClient();

                PagarMeService.DefaultApiKey = _configuration.GetValue<string>("Payment:PagarMe:ApiKey");
                PagarMeService.DefaultEncryptionKey = _configuration.GetValue<string>("Payment:PagarMe:EncryptionKey");

                Transaction transaction = new Transaction();

                //TODO - CALCULAR VALOR TOTAL
                transaction.Amount = Convert.ToInt32(value);
                transaction.PaymentMethod = PaymentMethod.Boleto;

                transaction.Customer = new Customer
                {
                    ExternalId = client.Id.ToString(),
                    Name = client.Name,
                    Type = CustomerType.Individual,
                    Country = "br",
                    Email = client.Email,
                    Documents = new[]
                      {
                        new Document {
                            Type = DocumentType.Cpf,
                            Number = Mask.Remove(client.CPF),
                        },
                    },
                    PhoneNumbers = new string[]
                      {
                        "+55" +
                        Mask.Remove(client.Phone),
                      },
                    Birthday = client.BirthDate.ToString("yyyy-MM-dd")
                };

                transaction.Save();

                return new { BoletoUrl = transaction.BoletoUrl, BoletoBarcode = transaction.BoletoBarcode, BoletoExpirationDate = transaction.BoletoExpirationDate };
            }
            catch (Exception e)
            {
                return new { Erro = e.Message };
            }

        }

        //public object GeneratePaymentCreditCard(CreditCard creditCard)
        //{
        //    Client client = _clientLogin.GetClient();
        //    PagarMeService.DefaultApiKey = _configuration.GetValue<string>("Payment:PagarMe:ApiKey");
        //    PagarMeService.DefaultEncryptionKey = _configuration.GetValue<string>("Payment:PagarMe:EncryptionKey");

        //    Card card = new Card();
        //    card.Number = creditCard.CardNumber;
        //    card.HolderName = creditCard.NameInCard;
        //    card.ExpirationDate = creditCard.Expiration;
        //    card.Cvv = creditCard.SecurityCode;

        //    card.Save();

        //    Transaction transaction = new Transaction();
        //    //TODO - CALCULAR VALOR TOTAL
        //    transaction.Amount = 2100;
        //    transaction.Card = new Card
        //    {
        //        Id = card.Id
        //    };

        //    transaction.Customer = new Customer
        //    {
        //        ExternalId = client.Id.ToString(),
        //        Name = client.Name,
        //        Type = CustomerType.Individual,
        //        Country = "br",
        //        Email = client.Email,
        //        Documents = new[]
        //          {
        //                new Document {
        //                    Type = DocumentType.Cpf,
        //                    Number = Mask.Remove(client.CPF),
        //                },
        //            },
        //        PhoneNumbers = new string[]
        //          {
        //                "+55" +
        //                Mask.Remove(client.Phone),
        //          },
        //        Birthday = client.BirthDate.ToString("yyyy-MM-dd")
        //    };

        //    transaction.Billing = new Billing
        //    {
        //        Name = client.Name,
        //        Address = new Address()
        //        {
        //            Country = "br",
        //            State = client.State,
        //            City = client.City,
        //            Neighborhood = client.Neighborhood,
        //            Street = client.Address + " " + client.Complement,
        //            StreetNumber = client.Number,
        //            Zipcode = Mask.Remove(client.Zipcode)
        //        }
        //    };

        //    var Today = DateTime.Now;

        //    transaction.Shipping = new Shipping
        //    {
        //        Name = client.Name,
        //        Fee = 100,
        //        DeliveryDate = Today.AddDays(4).ToString("yyyy-MM-dd"),
        //        Expedited = false,
        //        Address = new Address()
        //        {
        //            Country = "br",
        //            State = "sp",
        //            City = "Cotia",
        //            Neighborhood = "Rio Cotia",
        //            Street = "Rua Matrix",
        //            StreetNumber = "213",
        //            Zipcode = "04250000"
        //        }
        //    };

        //    transaction.Item = new[]
        //    {
        //      new Item()
        //      {
        //        Id = "1",
        //        Title = "Little Car",
        //        Quantity = 1,
        //        Tangible = true,
        //        UnitPrice = 1000
        //      },
        //      new Item()
        //      {
        //        Id = "2",
        //        Title = "Baby Crib",
        //        Quantity = 1,
        //        Tangible = true,
        //        UnitPrice = 1000
        //      }
        //    };

        //    transaction.Save();
        //}
    }
}
