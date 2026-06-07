using MasterBooking.BLL.DTO.AppointmentDto;
using MasterBooking.BLL.Services.AppointmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateAppointmentDto dto)
        {
            var appointment = await _appointmentService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetByClientId),
                new { clientId = appointment.ClientId },
                appointment);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClientId(string clientId)
        {
            var appointments =
                await _appointmentService.GetByClientIdAsync(clientId);

            return Ok(appointments);
        }

        [Authorize(Roles = "Master")]
        [HttpGet("master/{masterId}")]
        public async Task<IActionResult> GetByMasterId(string masterId)
        {
            var appointments =
                await _appointmentService.GetByMasterIdAsync(masterId);

            return Ok(appointments);
        }

        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _appointmentService.CancelAsync(id);

            return NoContent();
        }
    }
}
