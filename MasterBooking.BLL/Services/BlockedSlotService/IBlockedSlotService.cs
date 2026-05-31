using MasterBooking.BLL.DTO.BlockedSlotsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.BlockedSlotService
{
    public interface IBlockedSlotService
    {
        Task<BlockedSlotDto> CreateAsync(CreateBlockedSlotDto dto);

        Task<IEnumerable<BlockedSlotDto>> GetByMasterIdAsync(string masterId);

        Task DeleteAsync(int id);
    }
}
