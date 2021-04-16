using System.ComponentModel.DataAnnotations;

namespace Poc.Application.ViewModel.Identity
{
    public class LoginIdentityViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email em formato invalido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha deve ser preenchida!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}