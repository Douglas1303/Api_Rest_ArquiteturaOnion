using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Categories;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Categories
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, IResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = new CategoryModel(request.Descricao);
            try
            {
                _categoryRepository.Add(model);

                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CommandResult.Empty();
        }
    }
}