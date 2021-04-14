using System;
using System.Collections.Generic;

namespace Poc.Domain.Entities
{
    public class UserModel
    {
        public UserModel(Guid usuarioId, string nomeCompleto, string cpf, DateTime dataNascimento, string email)
        {
            UsuarioId = usuarioId; 
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Email = email;
            Ativo = true;
            DataCadastro = DateTime.Now;
        }

        //ctor protected para EF
        protected UserModel() { }

        public Guid UsuarioId { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public IEnumerable<EventModel> Eventos { get; private set; }
        public IEnumerable<SubscriptionModel> Inscricoes { get; private set; }
    }
}