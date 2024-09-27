using GMS.Service.Dtos;
using GMS.Service.Dtos.Diets;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Diets
{
    public interface IDietService
    {
        Task<ServiceResponse<DietDto>> GetDietByIdAsync(Guid id);

        Task<ServiceResponse<DietDto>> AddDietAsync(DietDto dietDto);

        Task<ServiceResponse<DietDto>> UpdateDietAsync(Guid id , DietDto dietDto);

        Task<ServiceResponse<bool>> DeleteDietAsync(Guid id);

        Task<ServiceResponse<IEnumerable<DietDto>>> GetAllDietsAsync(DietParametersDto queryParameters);
    }
}

