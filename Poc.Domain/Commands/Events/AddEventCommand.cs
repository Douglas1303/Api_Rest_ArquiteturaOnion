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

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Ativo { get; set; }
        public int? CategoriaId { get; set; }
    }
}