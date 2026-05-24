using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Services
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetByMasterIdAsync(string masterId)
        {
            return await _context.Services
                .Where(s => s.MasterId == masterId)
                .ToListAsync();
        }

        public async Task<List<Service>> SearchByNameAsync(string keyword)
        {
            return await _context.Services
                .Where(s => s.Name.Contains(keyword))
                .ToListAsync();
        }
    }
}
