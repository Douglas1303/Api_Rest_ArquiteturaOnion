using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Sponsor
{
    public class AddSponsorCommandHandler : IRequestHandler<AddSponsorCommand, IResult>
    {
        private readonly ISponsorRepository _sponsorRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddSponsorCommandHandlerRsc> Localizer;

        private const string AddSponsorError = "AddSponsorError";

        public AddSponsorCommandHandler(ISponsorRepository sponsorRepository, IUnitOfWork unitOfWork, IStringLocalizer<AddSponsorCommandHandlerRsc> localizer)
        {
            _sponsorRepository = sponsorRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(AddSponsorCommand request, CancellationToken cancellationToken)
        {
            var cepNumber = request.Cep.Replace("-", "").Replace(".", "");

            var dto = new SponsorDto(
                    request.NomePatrocinador,
                    request.Documento,
                    request.Telefone,
                    cepNumber,
                    request.Logradouro,
                    request.Complemento,
                    request.Bairro,
                    request.Localidade,
                    request.UF,
                    request.DDD); 
            try
            {
                await _sponsorRepository.AddAsync(dto);

                await _unitOfWork.Commit(); 
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(AddSponsorError));

                return cmdResult; 
            }

            return CommandResult.Empty(); 
        }
    }
}