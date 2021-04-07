using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCutting.Core.IPipelineBehavior
{
    public interface IDeepValidator<T> : IValidator<T>
    {
    }
}
