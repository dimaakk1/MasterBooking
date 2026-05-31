using MasterBooking.BLL.DTO.ReviewsDto;
using MasterBooking.BLL.DTO.ServicesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.ReviewService
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateAsync(CreateReviewDto dto);
        Task<IEnumerable<ReviewDto>> GetByMasterIdAsync(string masterId);
        Task<double> GetAverageRatingAsync(string masterId);
        
    }
}
