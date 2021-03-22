using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.Categories;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Categories
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, IResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddCategoryCommandHandlerRsc> Localizer;

        private const string CategoryError = "CategoryError"; 

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IStringLocalizer<AddCategoryCommandHandlerRsc> localizer)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer;
        }

        public async Task<IResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = new CategoryModel(request.Descricao);

            try
            {
                _categoryRepository.Add(model);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(CategoryError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}