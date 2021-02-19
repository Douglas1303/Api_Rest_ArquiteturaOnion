using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Users;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var model = new UserModel(request.NomeCompleto, request.Cpf, request.DataNascimento, request.Email);

            try
            {
                _userRepository.Add(model);

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