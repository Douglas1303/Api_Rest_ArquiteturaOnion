using Infra.CrossCutting.Core.CQRS;
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

        #region Constantes

        private const string AddFileError = "AddFileError";

        #endregion Constantes

        public FileApplication(IMediator mediator, IStringLocalizer<FileAppRsc> localizer)
        {
            _mediator = mediator;
            Localizer = localizer;
        }

        public async Task<IResult> AddAsync(AddFileViewModel viewModel)
        {
            try
            {
                var command = new AddFileCommand(
                    viewModel.TipoArquivoId,
                    viewModel.Arquivo.Name,
                    viewModel.Arquivo.FileName
                    );

                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(AddFileError));
                throw ex;
            }
        }
    }
}