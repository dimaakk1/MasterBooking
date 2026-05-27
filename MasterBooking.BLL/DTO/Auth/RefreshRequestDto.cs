using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.DTO.Auth
{
    public class RefreshRequestDto
    {
        public string RefreshToken { get; set; } = null!;
    }
}
