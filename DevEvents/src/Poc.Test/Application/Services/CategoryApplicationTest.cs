﻿using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Test.ObjectsFakers.Entities;
using Poc.Test.ObjectsFakers.ViewModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class CategoryApplicationTest
    {
        private readonly Mock<ICategoryRepository> _mockedCategoryRepository;
        private readonly Mock<IMediatorHandler> _mockedMediatorHandler;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly Mock<IStringLocalizer<CategoryAppRsc>> _mockedLocalizer;
        private readonly CategoryApplication _categoryApplication;

        public CategoryApplicationTest()
        {
            _mockedCategoryRepository = new Mock<ICategoryRepository>();
            _mockedMediatorHandler = new Mock<IMediatorHandler>();
            _mockedLog = new Mock<ILogModel>();
            _mockedLocalizer = new Mock<IStringLocalizer<CategoryAppRsc>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);

            _categoryApplication = new CategoryApplication(_mockedCategoryRepository.Object, _mockedMediatorHandler.Object,
                                                            _mapperFake, _mockedLog.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryIsValid_ReturnShouldBeOkWithList()
        {
            //Arrange
            _mockedCategoryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(CategoryModelFaker.GetListModelValid());

            //Act
            var requestResult = _categoryApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedCategoryRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            _mockedCategoryRepository.Setup(x => x.GetAllAsync()).Throws(new Exception());

            //Act
            var requestResult = _categoryApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedCategoryRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void AddCategory_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            var viewModel = AddCategoryViewModelFaker.GetViewModelValid();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).ReturnsAsync(commandResult);
            _mockedCategoryRepository.Setup(x => x.Add(It.IsAny<CategoryModel>())).Verifiable();

            //Act
            var requestResult = _categoryApplication.AddCategory(viewModel);

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void AddCategory_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            var viewModel = AddCategoryViewModelFaker.GetViewModelValid();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).Throws(new Exception());

            //Act
            var requestResult = _categoryApplication.AddCategory(viewModel);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }
    }
}