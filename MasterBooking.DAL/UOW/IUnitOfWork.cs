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
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; }

        IAppointmentRepository Appointments { get; }

        IAvailabilityRepository Availabilities { get; }

        IBlockedSlotRepository BlockedSlots { get; }

        IReviewRepository Reviews { get; }

        Task<int> SaveChangesAsync();
    }
}
