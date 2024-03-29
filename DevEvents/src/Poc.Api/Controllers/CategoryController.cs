﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [AllowAnonymous]
        [HttpGet("Categorias")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _categoryApplication.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] AddCategoryViewModel addCategoryViewModel)
        {
            return Ok(await _categoryApplication.AddCategory(addCategoryViewModel)); 
        }
    }
}