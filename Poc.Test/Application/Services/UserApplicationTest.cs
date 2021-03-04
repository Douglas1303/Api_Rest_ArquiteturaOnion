using AutoMapper;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using MediatR;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Interface.Repository;

namespace Poc.Test.Application.Services
{
    public class UserApplicationTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly Mock<IMediator> _mockedMediator;
        private readonly Mock<IMediatorHandler> _mockedMediatorHandler;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly UserApplication _userApplication;

        public UserApplicationTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mockedMediator = new Mock<IMediator>();
            _mockedMediatorHandler = new Mock<IMediatorHandler>();
            _mockedLog = new Mock<ILogModel>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config); 

            _userApplication = new UserApplication(_mockedUserRepository.Object, _mockedMediatorHandler.Object,
                                                    _mapperFake, _mockedLog.Object, _mockedMediator.Object);
        }
    }
}