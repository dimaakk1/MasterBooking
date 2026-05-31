using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.DTO.AppointmentDto
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public string ClientId { get; set; } = null!;
        public string MasterId { get; set; } = null!;

        public int ServiceId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = null!;
    }
}
