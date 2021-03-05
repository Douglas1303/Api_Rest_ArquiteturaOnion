using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.File;
using Poc.Domain.Interface.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.FIle
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, IResult>
    {
        private readonly IFileRepository _fileRepository; 
        public Task<IResult> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}