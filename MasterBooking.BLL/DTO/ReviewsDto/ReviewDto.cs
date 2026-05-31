using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.DTO.ReviewsDto
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string ClientId { get; set; } = null!;
        public string MasterId { get; set; } = null!;

        public int Rating { get; set; }
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
