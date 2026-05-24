using MasterBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Repositories.Users
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<List<ApplicationUser>> GetMastersAsync();
    }
}
