using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using Infra.CrossCutting.Models;
using MediatR.Pipeline;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Poc.Application.PipelineBehavior
{
    [ExcludeFromCodeCoverage]
    public class HandlerExceptionBehavior : RequestExceptionHandler<Command, IResult, Exception>
    {
        protected readonly ILogModel _log;

        public HandlerExceptionBehavior(ILogModel log)
        {
            _log = log;
        }

        protected override void Handle(Command request, Exception exception, RequestExceptionHandlerState<IResult> state)
        {
            IResult cmdResult = new CommandResult();

            cmdResult.AddErrorMessage("Unknown system error occurred.");

            _log.RecLog(nameof(HandlerExceptionBehavior), $"\n Command Name { request.GetType().Name } {exception.Message}, \n {exception.StackTrace}", LogType.LogError);

            state.SetHandled(cmdResult);
        }
    }
}