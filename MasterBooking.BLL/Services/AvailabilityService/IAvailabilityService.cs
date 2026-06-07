using MasterBooking.BLL.DTO.AvailabilityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.AvailabilityService
{
    public interface IAvailabilityService
    {
        Task<AvailabilityDto> CreateAsync(CreateAvailabilityDto dto);

        Task<IEnumerable<AvailabilityDto>> GetByMasterIdAsync(string masterId);

        Task DeleteAsync(int id);
    }
}
