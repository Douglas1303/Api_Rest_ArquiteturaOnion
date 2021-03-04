using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Categories;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class CategoryApplication : BaseApplicationService, ICategoryApplication
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStringLocalizer<CategoryAppRsc> Localizer;

        private const string GetAllCategoryError = "GetAllCategoryError";
        private const string AddCategoryError = "AddCategoryError";

        public CategoryApplication(ICategoryRepository categoryRepository, IMediatorHandler mediatorHandler, 
                                    IMapper mapper, ILogModel logModel, IStringLocalizer<CategoryAppRsc> stringSerializer) 
                                        : base(mediatorHandler, mapper, logModel)
        {
            _categoryRepository = categoryRepository;
            Localizer = stringSerializer; 
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAllAsync()));
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(GetAllCategoryError));
                throw ex;
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
                return new QueryResult(Localizer.GetMsg(AddCategoryError));
                throw ex; 
            }
        }
    }
}