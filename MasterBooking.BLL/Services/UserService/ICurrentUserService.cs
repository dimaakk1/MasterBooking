using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.UserService
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Role { get; }
    }
}
