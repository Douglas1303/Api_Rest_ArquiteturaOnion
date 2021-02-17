using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poc.Domain.Commands.Users
{
    public class AddUserCommand : Command
    {
        public override ValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}
