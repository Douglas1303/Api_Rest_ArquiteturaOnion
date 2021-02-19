using Infra.CrossCutting.Core.CQRS.Command;

namespace Poc.Domain.Commands.Sponsor
{
    public class AddSponsorCommand : Command
    {
        public AddSponsorCommand(string nomePatrocinador, string documento, string cep, string logradouro, string complemento, string bairro, string localidade, string uF, int dDD)
        {
            NomePatrocinador = nomePatrocinador;
            Documento = documento;
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uF;
            DDD = dDD;
        }

        public string NomePatrocinador { get; private set; }
        public string Documento { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }
        public int DDD { get; private set; }
    }
}