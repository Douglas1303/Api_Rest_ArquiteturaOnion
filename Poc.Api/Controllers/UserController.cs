using Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Entities.Identity;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet("Usuarios")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userApplication.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserViewModel addUserViewModel)
        {
            return Ok(await _userApplication.AddAsync(addUserViewModel)); 
        }

        [ClaimsAuthorize("Identity", "Incluir")]
        [HttpPost("Identity")] 
        public async Task<IActionResult> GetIdentity()
        {
            var tt = User.Identity.GetId(); 
            var aaaa = User.Identity.GetEmailUser(); 

            return Ok();
        }
    }
}