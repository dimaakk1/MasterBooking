using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.BlockedSlots
{
    public interface IBlockedSlotRepository : IGenericRepository<BlockedSlot>
    {
        Task<List<BlockedSlot>> GetByMasterIdAsync(string masterId);
        Task<bool> IsBlockedAsync(string masterId, DateTime start, DateTime end);

    }
}
