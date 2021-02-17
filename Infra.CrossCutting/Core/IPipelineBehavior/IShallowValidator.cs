using FluentValidation;

namespace Infra.CrossCutting.Core.IPipelineBehavior
{
    public interface IShallowValidator<T> : IValidator<T>
    {
    }
}