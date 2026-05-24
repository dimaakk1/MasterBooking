using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.Entities
{
    public class BlockedSlot
    {
        public int Id { get; set; }

        public string MasterId { get; set; } = null!;
        public ApplicationUser Master { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? Reason { get; set; }
    }
}
