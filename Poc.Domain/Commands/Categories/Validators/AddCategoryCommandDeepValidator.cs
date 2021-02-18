﻿using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poc.Domain.Commands.Categories.Validators
{
    public class AddCategoryCommandDeepValidator : BaseValidator<AddCategoryCommand, AddCategoryRsc>, IDeepValidator<AddCategoryCommand>
    {
        private const string DescriptionExists = "DescriptionExists"; 
        private readonly ICategoryRepository _categoryRepository;
        public AddCategoryCommandDeepValidator(IStringLocalizer<AddCategoryRsc> localizer, ICategoryRepository categoryRepository) : base(localizer)
        {
            _categoryRepository = categoryRepository;
            Validations(); 
        }

        private void Validations()
        {
            RuleFor(x => x.Descricao)
                .Must(CheckDescriptionExists)
                .WithErrorCode(DescriptionExists)
                .WithMessage(x => GetMessage(DescriptionExists)); 
        }

        private bool CheckDescriptionExists(string descricao)
        {
            return _categoryRepository.DescriptionExists(descricao); 
        }
    }
}