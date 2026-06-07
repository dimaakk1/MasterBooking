using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.DTO.ReviewsDto
{
    public class CreateReviewDto
    {
        public int AppointmentId { get; set; }

        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
