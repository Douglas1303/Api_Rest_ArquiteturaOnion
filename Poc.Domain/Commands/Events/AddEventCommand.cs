using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;
using System;

namespace Poc.Domain.Commands.Events
{
    public class AddEventCommand : Command
    {
        public AddEventCommand(string titulo, string descricao, DateTime dataInicio, DateTime dataFim, int? categoriaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Ativo = true;
            CategoriaId = categoriaId;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Ativo { get; private set; }
        public int? CategoriaId { get; private set; }
    }
}