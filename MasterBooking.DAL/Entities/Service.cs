using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Entities
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }

        public string MasterId { get; set; } = null!;
        public ApplicationUser Master { get; set; } = null!;
    }
}
