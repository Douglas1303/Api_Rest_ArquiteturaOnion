using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Sponsor
{
    public class AddSponsorCommandHandler : IRequestHandler<AddSponsorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSponsorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult> Handle(AddSponsorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}