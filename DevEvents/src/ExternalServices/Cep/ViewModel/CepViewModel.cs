using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExternalServices.Cep
{
    public class CepViewModel
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }


        [JsonProperty("cidade")]
        public string Localidade { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }

        [JsonProperty("ddd")]
        public string DDD { get; set; }
    }
}