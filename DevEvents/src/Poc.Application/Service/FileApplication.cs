using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.File;
using Poc.Domain.Resources.Application;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class FileApplication : IFileApplication
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<FileAppRsc> Localizer;
        private readonly ILogModel _log;

        #region Constantes

        private const string AddFileError = "AddFileError";

        #endregion Constantes

        public FileApplication(IMediator mediator, IStringLocalizer<FileAppRsc> localizer, ILogModel log)
        {
            _mediator = mediator;
            Localizer = localizer;
            _log = log;
        }

        public async Task<IResult> AddAsync(AddFileViewModel viewModel)
        {
            try
            {
                var command = new AddFileCommand(
                    viewModel.TipoArquivoId,
                    viewModel.Arquivo.ContentType,
                    viewModel.Arquivo.FileName
                    );

                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(AddFileError));
            }
        }
    }
}