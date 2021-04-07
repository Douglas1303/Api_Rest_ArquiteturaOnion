using Infra.CrossCutting.Core.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poc.Domain.Commands.File
{
    public class AddFileCommand : Command
    {
        public AddFileCommand(int tipoArquivoId, string nomeDeOrigem, string nomeParaSalvar)
        {
            TipoArquivoId = tipoArquivoId;
            NomeDeOrigem = nomeDeOrigem;
            NomeParaSalvar = nomeParaSalvar;
        }

        public int TipoArquivoId { get; private set; }
        public string NomeDeOrigem { get; private set; }
        public string NomeParaSalvar { get; private set; }
    }
}
