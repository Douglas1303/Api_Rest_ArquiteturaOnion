using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Helper.Interface;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApplication _userApplication;
        private readonly ICepService _cepService;
        private readonly IAuthenticatedUser _user;

        public UserController(IUserApplication userApplication, ICepService cepService, IAuthenticatedUser user)
        {
            _userApplication = userApplication;
            _cepService = cepService;
            _user = user;
        }

        [HttpGet("Usuarios")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userApplication.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserViewModel addUserViewModel)
        {
            var address = await _cepService.GetAddressAsync(addUserViewModel.Cep);

            var cep = new CepUserViewModel(address.Cep, address.Logradouro, address.Complemento, address.Bairro, address.Localidade, address.UF, address.DDD);

            return Ok(await _userApplication.AddAsync(addUserViewModel));
        }

        //[ClaimsAuthorize("Identity", "Incluir")]
        [HttpPost("Identity")]
        public async Task<IActionResult> GetIdentity()
        {
            var tt = User.Identity.GetId();
            var name = _user.EmailUser;
            var id = _user.UserId; 
            var aaaa = User.Identity.GetEmailUser();
            var user = User?.Claims;

            return Ok();
        }
    }
}