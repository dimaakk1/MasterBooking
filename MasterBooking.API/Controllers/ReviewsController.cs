using MasterBooking.BLL.DTO.ReviewsDto;
using MasterBooking.BLL.Services.ReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateReviewDto dto)
        {
            var review = await _reviewService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetByMasterId),
                new { masterId = review.MasterId },
                review);
        }

        [HttpGet("master/{masterId}")]
        public async Task<IActionResult> GetByMasterId(string masterId)
        {
            var reviews =
                await _reviewService.GetByMasterIdAsync(masterId);

            return Ok(reviews);
        }

        [HttpGet("master/{masterId}/rating")]
        public async Task<IActionResult> GetAverageRating(string masterId)
        {
            var rating =
                await _reviewService.GetAverageRatingAsync(masterId);

            return Ok(new { masterId, rating });
        }
    }
}
