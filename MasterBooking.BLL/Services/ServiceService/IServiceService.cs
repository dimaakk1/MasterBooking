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

        Task<ServiceDto?> GetByIdAsync(int id);

        Task<IEnumerable<ServiceDto>> GetByMasterIdAsync(string masterId);

        Task UpdateAsync(int id, UpdateServiceDto dto);

        Task DeleteAsync(int id);
    }
}
