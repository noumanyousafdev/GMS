using GMS.Service.Dtos;
using GMS.Service.Dtos.Measurements;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Measurements
{
    public interface IMeasurementService
    {
        Task<ServiceResponse<MeasurementDto>> CreateMeasuremntAsync(MeasurementDto measurementDto);
        Task<ServiceResponse<UpdateMeasurement>> UpdateMeasuremntAsync(UpdateMeasurement requestDto);
        Task<ServiceResponse<bool>> DeleteMeasuremntAsync(Guid measurementId);
        Task<ServiceResponse<MeasurementDto>> GetMeasuremntByIdAsync(Guid measurementId);
        Task<ServiceResponse<IEnumerable<MeasurementDto>>> GetAllMeasuremntAsync(MeasurementParametersDto queryParameters);
    }
}
