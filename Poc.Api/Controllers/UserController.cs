using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userApplication.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddUserViewModel addUserViewModel)
        {
            return Ok(await _userApplication.AddAsync(addUserViewModel)); 
        }
    }
}