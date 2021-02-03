using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
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

        public async Task<IResult> GetCategories()
        {
            try
            {
                return new QueryResult(await _categoryRepository.GetCategories());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}