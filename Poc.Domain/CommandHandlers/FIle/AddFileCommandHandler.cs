using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.File;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.FIle
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, IResult>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddFileCommandHandler(IFileRepository fileRepository, IUnitOfWork unitOfWork)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var dto = new FileDto(request.TipoArquivoId, request.NomeDeOrigem, request.NomeParaSalvar); 
            try
            {
                await _fileRepository.AddAsync(dto);

                await _unitOfWork.Commit(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CommandResult.Empty(); 
        }
    }
}