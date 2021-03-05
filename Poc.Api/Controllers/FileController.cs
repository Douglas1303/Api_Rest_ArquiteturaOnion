using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class FileController : BaseController
    {
        private readonly IFileApplication _fileApplication;

        public FileController(IFileApplication fileApplication)
        {
            _fileApplication = fileApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddFileViewModel viewModel)
        {
            return Ok(await _fileApplication.AddAsync(viewModel));
        }
    }
}