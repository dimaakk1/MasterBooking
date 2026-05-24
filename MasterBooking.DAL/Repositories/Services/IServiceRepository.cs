using MasterBooking.DAL.Repositories.Generic;
using MasterBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Services
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<List<Service>> GetByMasterIdAsync(string masterId);
        Task<List<Service>> SearchByNameAsync(string keyword);
    }
}
