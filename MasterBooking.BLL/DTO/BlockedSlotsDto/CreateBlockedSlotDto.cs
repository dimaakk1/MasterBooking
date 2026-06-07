using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.DTO.BlockedSlotsDto
{
    public class CreateBlockedSlotDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? Reason { get; set; }
    }
}
