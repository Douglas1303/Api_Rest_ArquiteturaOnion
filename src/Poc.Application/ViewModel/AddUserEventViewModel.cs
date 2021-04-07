using System.ComponentModel.DataAnnotations;

namespace Poc.Application.ViewModel
{
    public class AddUserEventViewModel
    {
        [Required]
        public int EventoId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
    }
}