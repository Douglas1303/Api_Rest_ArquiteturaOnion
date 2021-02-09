using Poc.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Poc.Domain.Entities
{
    public class UserModel : EntityBase
    {
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<EventModel> Eventos { get; set; }
        public IEnumerable<SubscriptionModel> Inscricoes { get; set; }

        public UserModel()
        {
            Ativo = true;
        }
    }
}