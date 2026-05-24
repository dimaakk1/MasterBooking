using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public string ClientId { get; set; } = null!;
        public ApplicationUser Client { get; set; } = null!;

        public string MasterId { get; set; } = null!;
        public ApplicationUser Master { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = null!;
    }
}
