using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;

        public ICollection<Appointment> ClientAppointments { get; set; } = new List<Appointment>();
        public ICollection<Appointment> MasterAppointments { get; set; } = new List<Appointment>();
    }
}
