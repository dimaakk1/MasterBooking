using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public string ClientId { get; set; } = null!;
        public ApplicationUser Client { get; set; } = null!;

        public string MasterId { get; set; } = null!;
        public ApplicationUser Master { get; set; } = null!;

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;

        public int Rating { get; set; }
        public string Comment { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
