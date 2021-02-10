using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExternalServices.Cep
{
    public class CepViewModel
    {
        [StringLength(8, ErrorMessage = "Cep deve ter 8 caracteres!")]
        public string Cep { get; set; }
    }
}
