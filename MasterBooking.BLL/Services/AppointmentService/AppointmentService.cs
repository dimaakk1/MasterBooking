using AutoMapper;
using MasterBooking.BLL.DTO.AppointmentDto;
using MasterBooking.BLL.Services.UserService;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(dto.ServiceId);

            if (service == null)
                throw new Exception("Service not found");

            var start = dto.StartTime;
            var end = start.AddMinutes(service.DurationMinutes);

            var isAvailable = await _unitOfWork.Availabilities
                .IsMasterAvailableAsync(service.MasterId, start, end);

            if (!isAvailable)
                throw new Exception("Master is not available");

            var isBlocked = await _unitOfWork.BlockedSlots
                .IsBlockedAsync(service.MasterId, start, end);

            if (isBlocked)
                throw new Exception("Time is blocked");

            var hasConflict = await _unitOfWork.Appointments
                .HasConflictAsync(service.MasterId, start, end);

            if (hasConflict)
                throw new Exception("Already booked");
            var clientId = _currentUser.UserId; 
            var appointment = new Appointment
            {
                ClientId = clientId,
                MasterId = service.MasterId,
                ServiceId = dto.ServiceId,
                StartTime = start,
                EndTime = end,
                Status = "Pending"
            };

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }
        public async Task<IEnumerable<AppointmentDto>> GetByClientIdAsync(string clientId)
        {
            var data = await _unitOfWork.Appointments.GetByClientIdAsync(clientId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(data);
        }

        public async Task<IEnumerable<AppointmentDto>> GetByMasterIdAsync(string masterId)
        {
            var data = await _unitOfWork.Appointments.GetByMasterIdAsync(masterId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(data);
        }

        public async Task CancelAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found");

            appointment.Status = "Cancelled";

            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
