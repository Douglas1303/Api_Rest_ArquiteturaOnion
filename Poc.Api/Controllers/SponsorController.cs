using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sponsorApplication.GetAll());
        }
    }
}