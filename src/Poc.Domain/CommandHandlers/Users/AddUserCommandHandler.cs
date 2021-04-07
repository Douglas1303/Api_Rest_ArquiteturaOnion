using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.Users;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddUserCommandHandlerRsc> Localizer;

        private const string AddUserError = "AddUserError"; 

        public AddUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IStringLocalizer<AddUserCommandHandlerRsc> localizer)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var model = new UserModel(request.NomeCompleto, request.Cpf, request.DataNascimento, request.Email);

            try
            {
                _userRepository.Add(model);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(AddUserError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}