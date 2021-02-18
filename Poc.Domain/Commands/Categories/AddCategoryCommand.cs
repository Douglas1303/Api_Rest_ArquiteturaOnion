using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;
using System;

namespace Poc.Domain.Commands.Categories
{
    public class AddCategoryCommand : Command
    {
        public AddCategoryCommand(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}