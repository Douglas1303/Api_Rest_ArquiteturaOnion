using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Entities.Identity;
using System.Threading.Tasks;

namespace Poc.Api.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventApplication _eventApplication;

        public EventController(IEventApplication eventApplication)
        {
            _eventApplication = eventApplication;
        }

        [AllowAnonymous]
        [HttpGet("Eventos")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _eventApplication.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("Evento/{id=int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _eventApplication.GetByIdAsync(id));
        }

        //[ClaimsAuthorize("Identity", "Incluir")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddEventViewModel addEventViewModel)
        {
            return Ok(await _eventApplication.AddAsync(addEventViewModel));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEventViewModel updateEventViewModel)
        {
            return Ok(await _eventApplication.UpdateAsync(updateEventViewModel));
        }

        [HttpPost("RegisterUserEvent")]
        public async Task<IActionResult> RegisterUserEvent([FromBody] AddUserEventViewModel addUserEventViewModel)
        {
            return Ok(await _eventApplication.RegisterAsync(addUserEventViewModel));
        }

        [HttpPut("Desabilitar/{id=int}")]
        public async Task<IActionResult> Disable(int id)
        {
            return Ok(await _eventApplication.CancelAsync(id));
        }


        [HttpDelete("Excluir/{id=int}")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await _eventApplication.RemoveAsync(id));
        }
    }
}