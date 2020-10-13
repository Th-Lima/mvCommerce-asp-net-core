using Microsoft.Extensions.Configuration;
using mvCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSCorreios;

namespace mvCommerce.Libraries.Manager.Freight
{
    public class WSCorreiosCalculateFreight
    {
        private IConfiguration _configuration;
        private CalcPrecoPrazoWSSoap _service;
        
        public WSCorreiosCalculateFreight(IConfiguration configuration, CalcPrecoPrazoWSSoap service)
        {
            _configuration = configuration;
            _service = service;
        }

        public async Task<ValueDeadlineFreight> CalculateFreight(string cepDestiny, string typeFreight, List<Package> packages)
        {
            List<ValueDeadlineFreight> valueOfPackages = new List<ValueDeadlineFreight>();

            foreach (var package in packages)
            {

                valueOfPackages.Add(await CalculateValueDeadLineFreight(cepDestiny, typeFreight, package));
            }

            ValueDeadlineFreight valuesOfFreights = valueOfPackages.GroupBy(a => a.TypeFreight).Select(list => new ValueDeadlineFreight
            {
                TypeFreight = list.First().TypeFreight,
                Deadline = list.Max(c => c.Deadline),
                Value = list.Sum(c => c.Value)
            }).ToList().First();

            return valuesOfFreights;
        }

        public async Task<ValueDeadlineFreight> CalculateValueDeadLineFreight(string cepDestiny, string typeFreight, Package package)
        {
            var cepOrigem = _configuration.GetValue<string>("CepOrigem");
            var maoPropria = _configuration.GetValue<string>("MaoPropria");
            var avisoRecebimento = _configuration.GetValue<string>("AvisoRecebimento");
            var diametro = Math.Max(Math.Max(package.Length, package.Width), package.Height);

            cResultado result =  await _service.CalcPrecoPrazoAsync("", 
                "", 
                typeFreight, 
                cepOrigem, 
                cepDestiny, 
                package.Weight.ToString(), 
                1, 
                package.Length, 
                package.Height, 
                package.Width, 
                diametro, 
                maoPropria, 
                0, 
                avisoRecebimento);

            if(result.Servicos[0].Erro == "0")
            {
                return new ValueDeadlineFreight()
                {
                    TypeFreight = typeFreight,
                    Deadline = int.Parse(result.Servicos[0].PrazoEntrega),
                    Value = double.Parse(result.Servicos[0].Valor.Replace(".", "").Replace(",", "."))
                };
            }
            else
            {
                throw new Exception("Erro" + result.Servicos[0].MsgErro);
            }

        }
    }
}
