﻿using Infra.CrossCutting.Core.CQRS.Command;
using System;

namespace Poc.Domain.Commands.Users
{
    public class AddUserCommand : Command
    {
        public AddUserCommand(string nomeCompleto, string cpf, DateTime dataNascimento, string email)
        {
            NomeCompleto = nomeCompleto;
            Cpf = cpf; 
            DataNascimento = dataNascimento;
            Email = email;
        }

        public string NomeCompleto { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
    }
}