using AutoMapper;
using MasterBooking.BLL.DTO.ServicesDto;
using MasterBooking.BLL.Services.UserService;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Services;
using MasterBooking.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            var masterId = _currentUser.UserId;

            var service = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DurationMinutes = dto.DurationMinutes,
                MasterId = masterId
            };

            await _unitOfWork.Services.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _unitOfWork.Services.GetAllAsync();

            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto?> GetByIdAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);

            if (service == null)
                return null;

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetByMasterIdAsync(string masterId)
        {
            var services = await _unitOfWork.Services.GetByMasterIdAsync(masterId);

            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task UpdateAsync(int id, UpdateServiceDto dto)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);

            if (service == null)
                throw new Exception("Service not found");

            _mapper.Map(dto, service);

            _unitOfWork.Services.Update(service);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);

            if (service == null)
                throw new Exception("Service not found");

            _unitOfWork.Services.Delete(service);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
