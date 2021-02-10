using System.ComponentModel.DataAnnotations;

namespace ExternalServices.Cep
{
    public class CepViewModel
    {
        [StringLength(8, ErrorMessage = "Cep deve ter 8 caracteres!")]
        public string Cep { get; set; }
    }
}