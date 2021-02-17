using FluentValidation;
using Infra.CrossCutting.Core.CQRS;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Core.IPipelineBehavior
{
    public abstract class BaseValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, IResult>
    {
        protected readonly IEnumerable<IValidator<TRequest>> Validators;

        protected BaseValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }

        public Task<IResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<IResult> next)
        {
            var failures = Validators.Select(v => v.Validate(request))
                .SelectMany(r => r.Errors)
                .Where(e => e != null)
                .Select(f => f?.ErrorMessage);

            return failures.Any() ? GetErrors(failures) : next();
        }

        private Task<IResult> GetErrors(IEnumerable<string> failures)
        {
            IResult result = new CommandResult(failures);
            return Task.FromResult(result);
        }
    }

    public class DeepValidatorBehavior<Command, TResponse> : BaseValidatorBehavior<Command, IResult>
    {
        public DeepValidatorBehavior(IEnumerable<IDeepValidator<Command>> validators) : base(validators)
        {
        }
    }

    public class ShallowValidatorBehavior<Command, TResponse> : BaseValidatorBehavior<Command, IResult>
    {
        public ShallowValidatorBehavior(IEnumerable<IShallowValidator<Command>> validators) : base(validators)
        {
        }
    }
}