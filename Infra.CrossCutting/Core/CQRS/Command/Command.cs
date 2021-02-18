using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Events;
using MediatR;
using System;

namespace Infra.CrossCutting.Core.CQRS.Command
{
    public abstract class Command : Message, IRequest<IResult>
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
            //Failures = new ValidationResult();
        }

        //public abstract ValidationResult Validate();

        //public ValidationResult Failures { get; protected set; }

        //public void AddFailure(string propertyName, string error)
        //{
        //    Failures.Errors.Add(new ValidationFailure(propertyName, error));
        //}
    }
}