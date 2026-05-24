using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Availabilitys
{
    public interface IAvailabilityRepository : IGenericRepository<Availability>
    {
        Task<List<Availability>> GetByMasterIdAsync(string masterId);
    }
}
