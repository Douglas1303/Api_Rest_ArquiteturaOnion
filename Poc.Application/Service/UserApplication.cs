using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class UserApplication : BaseApplicationService, IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork; 

        public UserApplication(IUserRepository userRepository, IMediatorHandler mediatorHandler, IUnitOfWork unitOfWork,  IMapper mapper, ILogModel logModel) : base(mediatorHandler, mapper, logModel)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork; 
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

        public async Task<IResult> AddAsync(AddUserViewModel addUserViewModel)
        {
            try
            {
                if (!new EmailVo(addUserViewModel.Email).IsValid()) return new QueryResult("Email invalido.");

                var model = new UserModel
                {
                    NomeCompleto = addUserViewModel.NomeCompleto,
                    DataNascimento = DateTime.Parse(addUserViewModel.DataNascimento),
                    Email = addUserViewModel.Email
                };

                 _userRepository.AddAsync(model);

                await _unitOfWork.Commit(); 

                return new CommandResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}