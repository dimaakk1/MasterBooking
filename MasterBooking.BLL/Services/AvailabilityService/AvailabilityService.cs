using AutoMapper;
using MasterBooking.BLL.DTO.AvailabilityDto;
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

        public AvailabilityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AvailabilityDto> CreateAsync(CreateAvailabilityDto dto)
        {
            var availability = _mapper.Map<Availability>(dto);

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

        public async Task DeleteAsync(Guid id)
        {
            var availability = await _unitOfWork.Availabilities.GetByIdAsync(id);

            if (availability == null)
                throw new Exception("Availability not found");

            _unitOfWork.Availabilities.Delete(availability);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
