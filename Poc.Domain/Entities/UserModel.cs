using Poc.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Poc.Domain.Entities
{
    public class UserModel : EntityBase
    {
        public UserModel(string nomeCompleto, DateTime dataNascimento, string email)
        {
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Email = email;
            Ativo = true;
        }

        //ctor protected para EF
        protected UserModel() { }

        public string NomeCompleto { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }
        public IEnumerable<EventModel> Eventos { get; private set; }
        public IEnumerable<SubscriptionModel> Inscricoes { get; private set; }
    }
}