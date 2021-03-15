using System.ComponentModel.DataAnnotations;

namespace Poc.Application.ViewModel.Identity
{
    public class UserIdentityViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email em formato invalido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha deve ser preenchida!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas informadas não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}