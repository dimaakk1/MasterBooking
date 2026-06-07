using AutoMapper;
using MasterBooking.BLL.DTO.AvailabilityDto;
using MasterBooking.BLL.Services.UserService;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.AvailabilityService
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;  
        public AvailabilityService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<AvailabilityDto> CreateAsync(CreateAvailabilityDto dto)
        {
            var masterId = _currentUser.UserId;

            var availability = new Availability
            {
                MasterId = masterId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _unitOfWork.Availabilities.AddAsync(availability);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AvailabilityDto>(availability);
        }

        public async Task<IEnumerable<AvailabilityDto>> GetByMasterIdAsync(string masterId)
        {
            var list = await _unitOfWork.Availabilities.GetAllAsync();

            var filtered = list
                .Where(x => x.MasterId == masterId);

            return _mapper.Map<IEnumerable<AvailabilityDto>>(filtered);
        }

        public async Task DeleteAsync(int id)
        {
            var availability = await _unitOfWork.Availabilities.GetByIdAsync(id);

            if (availability == null)
                throw new Exception("Availability not found");

            _unitOfWork.Availabilities.Delete(availability);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
