using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
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
        [HttpGet("Evento/{eventId}")]
        public async Task<IActionResult> GetById(int eventId)
        {
            return Ok(await _eventApplication.GetByIdAsync(eventId));
        }

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

        [HttpPost("{id}/eventos/{usuarioId}/inscrever")]
        public async Task<IActionResult> RegisterUserEvent(int eventId, int userId, [FromBody] AddUserEventViewModel addUserEventViewModel)
        {
            return Ok(await _eventApplication.RegisterAsync(eventId, userId, addUserEventViewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int eventId)
        {
            return Ok(await _eventApplication.CancelAsync(eventId));
        }
    }
}