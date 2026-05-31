using MasterBooking.BLL.DTO.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);

        Task<IEnumerable<AppointmentDto>> GetByClientIdAsync(string clientId);

        Task<IEnumerable<AppointmentDto>> GetByMasterIdAsync(string masterId);

        Task CancelAsync(int id);
    }
}
