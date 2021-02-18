using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;
using System;

namespace Poc.Domain.Commands.Events
{
    public class UpdateEventCommand : Command
    {
        public UpdateEventCommand(int id, string titulo, string descricao, DateTime dataInicio, DateTime dataFim, bool ativo, int? categoriaId)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Ativo = ativo;
            CategoriaId = categoriaId;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Ativo { get; set; }
        public int? CategoriaId { get; set; }
    }
}