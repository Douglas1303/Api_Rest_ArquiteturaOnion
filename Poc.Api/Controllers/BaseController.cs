using Infra.CrossCutting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Poc.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly LogModel _log;

        public BaseController()
        {
            _log = new LogModel();
        }
    }
}