using MasterBooking.BLL.DTO.ServicesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.ServiceService
{
    public interface IServiceService
    {
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);

        Task<IEnumerable<ServiceDto>> GetAllAsync();

        Task<ServiceDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<ServiceDto>> GetByMasterIdAsync(string masterId);

        Task UpdateAsync(Guid id, UpdateServiceDto dto);

        Task DeleteAsync(Guid id);
    }
}
