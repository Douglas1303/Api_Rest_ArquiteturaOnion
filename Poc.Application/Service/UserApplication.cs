using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using MediatR;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Users;
using Poc.Domain.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class UserApplication : BaseApplicationService, IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public UserApplication(IUserRepository userRepository, IMediatorHandler mediatorHandler, IMapper mapper, ILogModel logModel, IMediator mediator)
                                : base(mediatorHandler, mapper, logModel)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(await _userRepository.GetAllAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IResult> AddAsync(AddUserViewModel addUserViewModel, string email)
        {
            try
            {
                var command = new AddUserCommand(
                    addUserViewModel.NomeCompleto,
                    addUserViewModel.Cpf,
                    DateTime.Parse(addUserViewModel.DataNascimento),
                    email);

                return await _mediator.Send(command);
                //if (!new EmailVo(addUserViewModel.Email).IsValid()) return new QueryResult("Email invalido.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}