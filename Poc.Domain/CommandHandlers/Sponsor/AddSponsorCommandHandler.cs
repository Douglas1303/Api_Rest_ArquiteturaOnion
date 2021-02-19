using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Sponsor
{
    public class AddSponsorCommandHandler : IRequestHandler<AddSponsorCommand, IResult>
    {
        private readonly ISponsorRepository _sponsorRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public AddSponsorCommandHandler(ISponsorRepository sponsorRepository, IUnitOfWork unitOfWork)
        {
            _sponsorRepository = sponsorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(AddSponsorCommand request, CancellationToken cancellationToken)
        {
            var dto = new SponsorDto(
                    request.NomePatrocinador,
                    request.Documento,
                    request.Cep,
                    request.Logradouro,
                    request.Complemento,
                    request.Bairro,
                    request.Localidade,
                    request.UF,
                    request.DDD); 
            try
            {
                _sponsorRepository.Add(dto);

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