using MasterBooking.BLL.DTO.ServicesDto;
using MasterBooking.BLL.Services.ServiceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);

            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpGet("master/{masterId}")]
        public async Task<IActionResult> GetByMasterId(string masterId)
        {
            var services = await _serviceService.GetByMasterIdAsync(masterId);

            return Ok(services);
        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
        {
            var service = await _serviceService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = service.Id },
                service);
        }

        [HttpPost("createA")]
        public async Task<IActionResult> CreateA([FromBody] CreateServiceDto dto)
        {
            var service = await _serviceService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = service.Id },
                service);
        }

        [Authorize(Roles = "Master")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateServiceDto dto)
        {
            await _serviceService.UpdateAsync(id, dto);

            return NoContent();
        }

        [Authorize(Roles = "Master")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);

            return NoContent();
        }
    }
}
