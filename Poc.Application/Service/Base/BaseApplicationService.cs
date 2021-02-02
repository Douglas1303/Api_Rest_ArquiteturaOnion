using AutoMapper;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using System.Collections.Generic;
using System.Globalization;

namespace Poc.Application.Service.Base
{
    public abstract class BaseApplicationService
    {
        protected const string Success = "Éxito";
        protected readonly IMediatorHandler _mediatorHandler;
        protected readonly IMapper _mapper;
        protected List<string> Message;
        protected readonly ILogModel _log;

        protected BaseApplicationService(IMediatorHandler mediatorHandler, IMapper mapper, ILogModel log)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _log = log;
            Message = new List<string>();
            SetCurrentCulture();
        }

        protected BaseApplicationService(IMapper mapper, ILogModel log) : this(null, mapper, log)
        {
        }

        private void SetCurrentCulture()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR", false);
        }
    }
}