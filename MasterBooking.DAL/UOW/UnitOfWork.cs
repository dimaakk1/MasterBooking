using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Repositories.Appointments;
using MasterBooking.DAL.Repositories.Availabilitys;
using MasterBooking.DAL.Repositories.BlockedSlots;
using MasterBooking.DAL.Repositories.Reviews;
using MasterBooking.DAL.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IServiceRepository Services { get; }

        public IAppointmentRepository Appointments { get; }

        public IAvailabilityRepository Availabilities { get; }

        public IBlockedSlotRepository BlockedSlots { get; }

        public IReviewRepository Reviews { get; }

        public UnitOfWork(
            AppDbContext context,
            IServiceRepository serviceRepository,
            IAppointmentRepository appointmentRepository,
            IAvailabilityRepository availabilityRepository,
            IBlockedSlotRepository blockedSlotRepository,
            IReviewRepository reviewRepository)
        {
            _context = context;

            Services = serviceRepository;
            Appointments = appointmentRepository;
            Availabilities = availabilityRepository;
            BlockedSlots = blockedSlotRepository;
            Reviews = reviewRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
