namespace Poc.Domain.Dtos
{
    public class SponsorDto
    {
        public SponsorDto(string nomePatrocinador, string document, string cep, string logradouro, string complemento, string bairro, string localidade, string uF, int dDD)
        {
            NomePatrocinador = nomePatrocinador;
            Documento = document;
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uF;
            DDD = dDD;
        }

        public SponsorDto(int id, string nomePatrocinador, string document, string cep, string logradouro, string complemento, string bairro, string localidade, string uF, int dDD)
        {
            Id = id;
            NomePatrocinador = nomePatrocinador;
            Documento = document;
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uF;
            DDD = dDD;
        }

        public SponsorDto() { }

        public int Id { get; private set; }
        public string NomePatrocinador { get; private set; }
        public string Documento { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }
        public int DDD { get; private set; }

        public string ChangeComplement(string complemento)
        {
            Complemento = complemento;

            return Complemento; 
         }
    }
}