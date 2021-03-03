using AutoMapper;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Refit;
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
            var cepClient = RestService.For<ICepService>("http://viacep.com.br");

            var address = await cepClient.GetAddressAsync(addUserViewModel.Cep);

            var cep = new CepUserViewModel(address.Cep, address.Logradouro, address.Complemento, address.Bairro, address.Localidade, address.UF, address.DDD); 

            return Ok(await _userApplication.AddAsync(addUserViewModel, User.Identity.GetEmailUser()));
        }

        //[ClaimsAuthorize("Identity", "Incluir")]
        [HttpPost("Identity")]
        public async Task<IActionResult> GetIdentity()
        {
            var tt = User.Identity.GetId();
            var aaaa = User.Identity.GetEmailUser();

            return Ok();
        }
    }
}