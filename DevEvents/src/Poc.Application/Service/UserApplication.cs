﻿using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Users;
using Poc.Domain.Helper.Interface;
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
        private readonly IAuthenticatedUser _user;

        #region
        private const string GetAllUserError = "GetAllUserError"; 
        private const string AddUserError = "AddUserError"; 
        #endregion

        public UserApplication(IUserRepository userRepository, IMediatorHandler mediatorHandler, IMapper mapper, 
                                ILogModel logModel, IMediator mediator, IStringLocalizer<UserAppRsc> localizer, IAuthenticatedUser user)
                                : base(mediatorHandler, mapper, logModel)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            Localizer = localizer;
            _user = user; 
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(_mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAllAsync()));
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(GetAllUserError));
            }
        }

        public async Task<IResult> AddAsync(AddUserViewModel addUserViewModel)
        {
            try
            {
                var command = new AddUserCommand(
                    _user.UserId,
                    addUserViewModel.NomeCompleto,
                    addUserViewModel.Cpf,
                    DateTime.Parse(addUserViewModel.DataNascimento),
                    _user.EmailUser);

                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(AddUserError)); 
            }
        }
    }
}