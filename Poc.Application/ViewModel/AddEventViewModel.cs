using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Poc.Application.ViewModel
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Titulo precisa ser informado.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Descrição precisa ser informada.")]
        public string Descricao { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }

    }
}
