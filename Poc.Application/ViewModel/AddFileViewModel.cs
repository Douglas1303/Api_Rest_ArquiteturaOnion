using Microsoft.AspNetCore.Http;

namespace Poc.Application.ViewModel
{
    public class AddFileViewModel
    {
        public IFormFile Arquivo { get; set; }
        public int TipoArquivoId { get; set; }
    }
}