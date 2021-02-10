using ExternalServices.Cep;
using ExternalServices.Cep.Interface;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class CepController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CepViewModel cep)
        {
            try
            {
                var cepClient = RestService.For<ICepService>("http://viacep.com.br");

                var address = await cepClient.GetAddressAsync(cep.Cep);

                return Ok(address);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}