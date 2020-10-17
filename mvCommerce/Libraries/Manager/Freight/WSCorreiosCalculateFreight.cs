using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Configuration;
using mvCommerce.Models;
using mvCommerce.Models.Constants;
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
                var result = await CalculateValueDeadLineFreight(cepDestiny, typeFreight, package);

                if (result != null)
                {
                    valueOfPackages.Add(result);
                }
            }

            if (valueOfPackages.Count > 0)
            {
                ValueDeadlineFreight valuesOfFreights = valueOfPackages.GroupBy(a => a.TypeFreight).Select(list => new ValueDeadlineFreight
                {
                    TypeFreight = list.First().TypeFreight,
                    ServiceCode = list.First().ServiceCode,
                    Deadline = list.Max(c => c.Deadline),
                    Value = list.Sum(c => c.Value)
                }).ToList().First();

                return valuesOfFreights;
            }
            return null;
        }

        public async Task<ValueDeadlineFreight> CalculateValueDeadLineFreight(string cepDestiny, string typeFreight, Package package)
        {
            var cepOrigem = _configuration.GetValue<string>("Freight:CepOrigem");
            var maoPropria = _configuration.GetValue<string>("Freight:MaoPropria");
            var avisoRecebimento = _configuration.GetValue<string>("Freight:AvisoRecebimento");
            var diametro = Math.Max(Math.Max(package.Length, package.Width), package.Height);

            cResultado result = await _service.CalcPrecoPrazoAsync("",
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

            if (result.Servicos[0].Erro == "0")
            {
                var cleanValue = result.Servicos[0].Valor.Replace(".", "");
                var finalValue = double.Parse(cleanValue);


                return new ValueDeadlineFreight()
                {
                    TypeFreight = TypeFreightConstant.GetNames(typeFreight),
                    ServiceCode = typeFreight,
                    Deadline = int.Parse(result.Servicos[0].PrazoEntrega),
                    Value = finalValue
                    };
            }
            else if (result.Servicos[0].Erro == "008" || result.Servicos[0].Erro == "-888")
            {
                //SEDEX10 - Don't delivery in region
                return null;
            }
            else
            {
                throw new Exception("Erro" + result.Servicos[0].MsgErro);
            }

        }
    }
}
