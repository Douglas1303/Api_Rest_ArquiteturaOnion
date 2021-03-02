using Infra.CrossCutting.Core.CQRS.Command;
using Poc.Domain.Enum;

namespace Poc.Domain.Commands.Sponsor
{
    public class AddSponsorCommand : Command
    {
        public AddSponsorCommand(ETipoPatrocinador tipoPatrocinador, string nomePatrocinador, string documento, string telefone, string cep, string logradouro, string complemento, string bairro, string localidade, string uf, int ddd)
        {
            TipoPatrocinador = tipoPatrocinador;
            NomePatrocinador = nomePatrocinador;
            Documento = documento;
            Telefone = telefone; 
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uf;
            DDD = ddd;
        }

        public ETipoPatrocinador TipoPatrocinador { get; set; }
        public string NomePatrocinador { get; private set; }
        public string Documento { get; private set; }
        public string Telefone { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }
        public int DDD { get; private set; }
    }
}