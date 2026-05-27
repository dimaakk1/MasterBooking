using MasterBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.Auth
{
    public interface ITokenService
    {
        Task<string> CreateAccessTokenAsync(ApplicationUser user);

        string GenerateRefreshToken();
    }
}
