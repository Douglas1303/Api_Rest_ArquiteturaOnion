using ExternalServices.Cep;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using System;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class CepController : BaseController
    {
        private readonly ICepApplication _cepApplidation;

        public CepController(ICepApplication cepApplidation)
        {
            _cepApplidation = cepApplidation;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetByCepAsync(string cep)
        {
            try
            {
                return Ok(await _cepApplidation.GetCepAsync(cep));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}