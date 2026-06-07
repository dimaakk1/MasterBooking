using AutoMapper;
using MasterBooking.BLL.DTO.ReviewsDto;
using MasterBooking.BLL.Services.UserService;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto dto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);

            if (appointment == null)
                throw new Exception("Appointment not found");

            if (appointment.Status != "Completed")
                throw new Exception("You can review only completed appointments");

            var alreadyReviewed = await _unitOfWork.Reviews
                .HasReviewForAppointmentAsync(dto.AppointmentId);

            if (alreadyReviewed)
                throw new Exception("Review already exists");

            var clientId = _currentUser.UserId;

            var review = new Review
            {
                ClientId = clientId,
                MasterId = appointment.MasterId,
                AppointmentId = dto.AppointmentId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetByMasterIdAsync(string masterId)
        {
            var reviews = await _unitOfWork.Reviews.GetByMasterIdAsync(masterId);

            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<double> GetAverageRatingAsync(string masterId)
        {
            var reviews = await _unitOfWork.Reviews.GetByMasterIdAsync(masterId);

            if (!reviews.Any())
                return 0;

            return reviews.Average(x => x.Rating);
        }
    }
}
