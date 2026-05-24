using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.BlockedSlots
{
    public class BlockedSlotRepository : GenericRepository<BlockedSlot>, IBlockedSlotRepository
    {
        private readonly AppDbContext _context;

        public BlockedSlotRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BlockedSlot>> GetByMasterIdAsync(string masterId)
        {
            return await _context.BlockedSlots
                .Where(b => b.MasterId == masterId)
                .ToListAsync();
        }
    }
}
