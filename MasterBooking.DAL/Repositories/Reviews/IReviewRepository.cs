using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Reviews
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetByMasterIdAsync(string masterId);
        Task<bool> HasReviewForAppointmentAsync(int appointmentId);

    }
}
