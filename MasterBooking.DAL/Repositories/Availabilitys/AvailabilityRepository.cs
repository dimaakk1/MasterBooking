using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Availabilitys
{
    public class AvailabilityRepository : GenericRepository<Availability>, IAvailabilityRepository
    {
        private readonly AppDbContext _context;

        public AvailabilityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Availability>> GetByMasterIdAsync(string masterId)
        {
            return await _context.Availabilities
                .Where(a => a.MasterId == masterId)
                .ToListAsync();
        }
    }
}
