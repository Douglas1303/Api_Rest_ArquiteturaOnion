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

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Ativo { get; private set; }
        public int? CategoriaId { get; private set; }
    }
}