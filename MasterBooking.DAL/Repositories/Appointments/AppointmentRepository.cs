using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Appointments
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetByClientIdAsync(string clientId)
        {
            return await _context.Appointments
                .Where(a => a.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetByMasterIdAsync(string masterId)
        {
            return await _context.Appointments
                .Where(a => a.MasterId == masterId)
                .ToListAsync();
        }

        public async Task<bool> IsTimeSlotTakenAsync(string masterId, DateTime start, DateTime end)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.MasterId == masterId &&
                a.StartTime < end &&
                a.EndTime > start
            );
        }
    }
}
