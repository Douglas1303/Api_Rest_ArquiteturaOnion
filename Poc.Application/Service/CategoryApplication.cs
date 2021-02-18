using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Categories;
using Poc.Domain.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class CategoryApplication : BaseApplicationService, ICategoryApplication
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplication(ICategoryRepository categoryRepository, IMediatorHandler mediatorHandler, IMapper mapper, ILogModel logModel) : base(mediatorHandler, mapper, logModel)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(await _categoryRepository.GetAllAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IResult> AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            try
            {
                var command = new AddCategoryCommand(addCategoryViewModel.Descricao);

                return await _mediatorHandler.SendCommand(command); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}