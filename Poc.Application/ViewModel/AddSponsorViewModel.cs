namespace Poc.Application.ViewModel
{
    public class AddSponsorViewModel
    {
        public string NomePatrocinador { get; set; }
        public string Documento { get; set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }
        public int DDD { get; private set; }
    }
}