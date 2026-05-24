using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Reviews
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetByMasterIdAsync(string masterId)
        {
            return await _context.Reviews
                .Where(r => r.MasterId == masterId)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(string masterId)
        {
            return await _context.Reviews
                .Where(r => r.MasterId == masterId)
                .Select(r => (double?)r.Rating)
                .AverageAsync() ?? 0;
        }
    }
}
