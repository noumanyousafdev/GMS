using GMS.Service.Dtos;
using GMS.Service.Dtos.Halls;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Halls
{
    public interface IHallService
    {
        Task<ServiceResponse<HallDto>> GetHallByIdAsync(Guid id);
        Task<ServiceResponse<HallDto>> AddHallAsync(HallDto hallDto);
        Task<ServiceResponse<HallDto>> UpdateHallAsync(HallDto hallDto);
        Task<ServiceResponse<IEnumerable<HallDto>>> GetAllHallsAsync(HallParametersDto queryParameters);
        Task<ServiceResponse<bool>> DeleteHallAsync(Guid id);
    }
}
