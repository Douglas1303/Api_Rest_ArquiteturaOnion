using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class EventApplicationTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IMediatorHandler> _mockedMediatorHandler;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly Mock<IStringLocalizer<EventAppRsc>> _mockedLocalizer;
        private readonly EventApplication _eventApplication;

        public EventApplicationTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedMediatorHandler = new Mock<IMediatorHandler>();
            _mockedLog = new Mock<ILogModel>();
            _mockedLocalizer = new Mock<IStringLocalizer<EventAppRsc>>(); 

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config); 

            _eventApplication = new EventApplication(_mockedEventRepository.Object, _mockedMediatorHandler.Object, _mapperFake,
                                                     _mockedLog.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryIsValid_ReturnShouldBeOkWithList()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(GetEventModel());

            //Act
            var requestResult = _eventApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetAllAsync()).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        private List<EventModel> GetEventModel()
        {
            return new List<EventModel>
            {
                new EventModel("Aulão", "Aulão de banco de dados", DateTime.Parse("10/01/2030"), DateTime.Parse("14/01/2030"), 1),
                new EventModel("Bootcamp", "Bootcamp DotNet", DateTime.Parse("10/01/2030"), DateTime.Parse("14/01/2030"), 1)
            }; 
        }
    }
}