using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface.Identity;
using Poc.Application.ViewModel.Identity;
using System;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationApplication _authorizationApplication;

        public AuthorizationController(IAuthorizationApplication authorizationApplication)
        {
            _authorizationApplication = authorizationApplication;
        }

        /// <summary>
        /// Verificar se Api está funcionando
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("AuthorizationController :: Acessado em : " + DateTime.Now.ToLongDateString());
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserIdentityViewModel viewModel)
        {
            return Ok(await _authorizationApplication.RegisterUserAsync(viewModel));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginIdentityViewModel viewModel)
        {
            return Ok(await _authorizationApplication.LoginAsync(viewModel));
        }
    }
}