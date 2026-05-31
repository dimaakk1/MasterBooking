using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.DTO.AppointmentDto
{
    public class CreateAppointmentDto
    {
        public string ClientId { get; set; } = null!;
        public int ServiceId { get; set; }

        public DateTime StartTime { get; set; }
    }
}
