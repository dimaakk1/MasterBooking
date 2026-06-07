using MasterBooking.BLL.DTO.BlockedSlotsDto;
using MasterBooking.BLL.Services.BlockedSlotService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedSlotsController : ControllerBase
    {
        private readonly IBlockedSlotService _blockedSlotService;

        public BlockedSlotsController(IBlockedSlotService blockedSlotService)
        {
            _blockedSlotService = blockedSlotService;
        }

        [HttpGet("master/{masterId}")]
        public async Task<IActionResult> GetByMasterId(string masterId)
        {
            var slots = await _blockedSlotService
                .GetByMasterIdAsync(masterId);

            return Ok(slots);
        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateBlockedSlotDto dto)
        {
            var slot = await _blockedSlotService
                .CreateAsync(dto);

            return Ok(slot);
        }

        [Authorize(Roles = "Master")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blockedSlotService.DeleteAsync(id);

            return NoContent();
        }
    }
}
