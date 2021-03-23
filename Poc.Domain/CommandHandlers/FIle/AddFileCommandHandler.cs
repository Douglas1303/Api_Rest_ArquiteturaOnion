using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.File;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.FIle
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, IResult>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddFileCommandHandlerRsc> Localizer;

        private const string AddFileError = "AddFileError"; 

        public AddFileCommandHandler(IFileRepository fileRepository, IUnitOfWork unitOfWork, IStringLocalizer<AddFileCommandHandlerRsc> localizer)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var dto = new FileDto(request.TipoArquivoId, request.NomeDeOrigem, request.NomeParaSalvar); 
            try
            {
                await _fileRepository.AddAsync(dto);

                await _unitOfWork.Commit(); 
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(AddFileError));

                return cmdResult; 
            }

            return CommandResult.Empty(); 
        }
    }
}