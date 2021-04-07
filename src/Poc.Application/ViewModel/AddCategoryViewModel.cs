using System.ComponentModel.DataAnnotations;

namespace Poc.Application.ViewModel
{
    public class AddCategoryViewModel
    {
        [Required]
        public string Descricao { get; set; }
    }
}