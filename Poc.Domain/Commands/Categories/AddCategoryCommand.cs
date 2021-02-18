using Infra.CrossCutting.Core.CQRS.Command;

namespace Poc.Domain.Commands.Categories
{
    public class AddCategoryCommand : Command
    {
        public AddCategoryCommand(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; private set; }
    }
}