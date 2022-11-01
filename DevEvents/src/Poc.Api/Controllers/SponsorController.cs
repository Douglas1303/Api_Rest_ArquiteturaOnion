using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class SponsorController : BaseController
    {
        private readonly ISponsorApplication _sponsorApplication;

        public SponsorController(ISponsorApplication sponsorApplication)
        {
            _sponsorApplication = sponsorApplication;
        }

        [HttpGet("{id}/{teste}")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sponsorApplication.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddSponsorViewModel viewModel)
        {
            return Ok(await _sponsorApplication.AddAsync(viewModel)); 
        }

        [HttpDelete("Excluir/{id=int}")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await _sponsorApplication.RemoveAsync(id)); 
        }
    }
}