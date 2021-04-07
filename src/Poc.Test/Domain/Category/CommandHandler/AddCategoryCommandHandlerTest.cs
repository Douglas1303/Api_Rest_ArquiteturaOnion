using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.Categories;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.Category.CommandHandler
{
    public class AddCategoryCommandHandlerTest
    {
        private readonly Mock<ICategoryRepository> _mockedCategoryRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<AddCategoryCommandHandlerRsc>> _mockedLocalizer;
        private readonly AddCategoryCommandHandler _commandHandler;

        public AddCategoryCommandHandlerTest()
        {
            _mockedCategoryRepository = new Mock<ICategoryRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<AddCategoryCommandHandlerRsc>>();

            _commandHandler = new AddCategoryCommandHandler(_mockedCategoryRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = AddCategoryCommandFaker.GetCommandValid();
            _mockedCategoryRepository.Setup(x => x.Add(It.IsAny<CategoryModel>())).Verifiable();

            //Act
            var response = _commandHandler.Handle(command, new System.Threading.CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Ok.ToString());
        }

        [Fact]
        public void Handler_WhenRepositoryReturnError_ReturnShoulBeException()
        {
            //Arrange
            var command = AddCategoryCommandFaker.GetCommandValid();
            _mockedCategoryRepository.Setup(x => x.Add(It.IsAny<CategoryModel>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}