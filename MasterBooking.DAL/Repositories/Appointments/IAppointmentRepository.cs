using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Appointments
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<List<Appointment>> GetByClientIdAsync(string clientId);
        Task<List<Appointment>> GetByMasterIdAsync(string masterId);

        Task<bool> IsTimeSlotTakenAsync(string masterId, DateTime start, DateTime end);
    }
}
