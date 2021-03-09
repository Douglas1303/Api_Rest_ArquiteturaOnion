using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Users;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class UserApplication : BaseApplicationService, IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<UserAppRsc> Localizer;

        #region
        private const string GetAllUserError = "GetAllUserError"; 
        private const string AddUserError = "AddUserError"; 
        #endregion

        public UserApplication(IUserRepository userRepository, IMediatorHandler mediatorHandler, IMapper mapper, 
                                ILogModel logModel, IMediator mediator, IStringLocalizer<UserAppRsc> localizer)
                                : base(mediatorHandler, mapper, logModel)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            Localizer = localizer; 
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(_mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAllAsync()));
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(GetAllUserError));
                throw ex;
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
                return new QueryResult(Localizer.GetMsg(AddUserError)); 
                throw ex;
            }
        }
    }
}