using System;
using System.ComponentModel.DataAnnotations;

namespace Poc.Application.ViewModel
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Nome não pode ser vazio.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Data de nascimento não pode ser vazia.")]
        public string DataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "Email em formato invalido.")]
        public string Email { get; set; }
    }
}