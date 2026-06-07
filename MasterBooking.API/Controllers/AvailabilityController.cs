using MasterBooking.BLL.DTO.AvailabilityDto;
using MasterBooking.BLL.Services.AvailabilityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet("master/{masterId}")]
        public async Task<IActionResult> GetByMasterId(string masterId)
        {
            var availabilities =
                await _availabilityService.GetByMasterIdAsync(masterId);

            return Ok(availabilities);
        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAvailabilityDto dto)
        {
            var availability =
                await _availabilityService.CreateAsync(dto);

            return Ok(availability);
        }

        [Authorize(Roles = "Master")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _availabilityService.DeleteAsync(id);

            return NoContent();
        }
    }
}
