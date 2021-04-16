using AutoMapper;
using Bogus;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Entities;
using Poc.Domain.Helper.Interface;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Test.ObjectsFakers.Entities;
using Poc.Test.ObjectsFakers.ViewModel;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class UserApplicationTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly Mock<IMediator> _mockedMediator;
        private readonly Mock<IMediatorHandler> _mockedMediatorHandler;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly Mock<IAuthenticatedUser> _mockedUser;
        private readonly Mock<IStringLocalizer<UserAppRsc>> _mockedLocalizer;
        private readonly UserApplication _userApplication;

        private readonly Faker _faker;

        public UserApplicationTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mockedMediator = new Mock<IMediator>();
            _mockedMediatorHandler = new Mock<IMediatorHandler>();
            _mockedLog = new Mock<ILogModel>();
            _mockedUser = new Mock<IAuthenticatedUser>(); 
            _mockedLocalizer = new Mock<IStringLocalizer<UserAppRsc>>();

            _faker = new Faker();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);

            _userApplication = new UserApplication(_mockedUserRepository.Object, _mockedMediatorHandler.Object,
                                                    _mapperFake, _mockedLog.Object, _mockedMediator.Object,
                                                    _mockedLocalizer.Object, _mockedUser.Object);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryIsValid_ReturnShouldBeOkWithList()
        {
            //Arrange
            var model = UserModelFaker.GetModelValid();
            _mockedUserRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(model);

            //Act
            var requestResult = _userApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedUserRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            _mockedUserRepository.Setup(x => x.GetAllAsync()).Throws(new Exception());

            //Act
            var requestResult = _userApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedUserRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void AddAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediator.Setup(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            _mockedUserRepository.Setup(x => x.Add(It.IsAny<UserModel>())).Verifiable();
            _mockedUserRepository.Setup(x => x.UserExists(It.IsAny<string>())).Returns(true);

            //Act
            var requestResult = _userApplication.AddAsync(AddUserViewModelFaker.GetViewModelValid());

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void AddAsync_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediator.Setup(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var requestResult = _userApplication.AddAsync(AddUserViewModelFaker.GetViewModelValid());

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }
    }
}