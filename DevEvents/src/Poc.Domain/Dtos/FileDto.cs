namespace Poc.Domain.Dtos
{
    public class FileDto : DtoBase
    {
        public FileDto(int tipoArquivoId, string nomeDeOrigem, string nomeParaSalvar)
        {
            TipoArquivoId = tipoArquivoId;
            NomeDeOrigem = nomeDeOrigem;
            NomeParaSalvar = nomeParaSalvar;
            Ativo = true;
        }

        private FileDto(){}

        public int TipoArquivoId { get; private set; }
        public string NomeDeOrigem { get; private set; }
        public string NomeParaSalvar { get; private set; }
        public bool Ativo { get; private set; }
    }
}