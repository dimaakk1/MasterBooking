using AutoMapper;
using MasterBooking.BLL.DTO.BlockedSlotsDto;
using MasterBooking.BLL.Services.UserService;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.BlockedSlotService
{
    public class BlockedSlotService : IBlockedSlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public BlockedSlotService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BlockedSlotDto> CreateAsync(CreateBlockedSlotDto dto)
        {
            // 1. validation
            if (dto.StartTime >= dto.EndTime)
                throw new Exception("Invalid time range");
            var masterId = _currentUser.UserId; //  з JWT
            // 2. conflict with existing blocked slots (SQL)
            var hasBlockedConflict = await _unitOfWork.BlockedSlots
                .IsBlockedAsync(masterId, dto.StartTime, dto.EndTime);

            if (hasBlockedConflict)
                throw new Exception("Blocked slot overlaps existing one");

            // 3. conflict with appointments (SQL)
            var hasAppointmentConflict = await _unitOfWork.Appointments
                .HasConflictAsync(masterId, dto.StartTime, dto.EndTime);

            if (hasAppointmentConflict)
                throw new Exception("Cannot block time with existing appointments");

            // 4. create entity
            var blockedSlot = _mapper.Map<BlockedSlot>(dto);

            await _unitOfWork.BlockedSlots.AddAsync(blockedSlot);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BlockedSlotDto>(blockedSlot);
        }

        public async Task<IEnumerable<BlockedSlotDto>> GetByMasterIdAsync(string masterId)
        {
            var slots = await _unitOfWork.BlockedSlots
                .GetByMasterIdAsync(masterId);

            return _mapper.Map<IEnumerable<BlockedSlotDto>>(slots);
        }

        public async Task DeleteAsync(int id)
        {
            var slot = await _unitOfWork.BlockedSlots.GetByIdAsync(id);

            if (slot == null)
                throw new Exception("Blocked slot not found");

            _unitOfWork.BlockedSlots.Delete(slot);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
