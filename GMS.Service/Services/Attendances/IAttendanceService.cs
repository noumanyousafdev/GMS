using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Workout;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Attendances
{
    public interface IAttendanceService
    {
        Task<ServiceResponse<AttendanceDto>> CreateAttendanceAsync(AttendanceDto attendanceDto);
        Task<ServiceResponse<UpdateAttendance>> UpdateAttendanceAsync(UpdateAttendance requestDto);
        Task<ServiceResponse<bool>> DeleteAttendanceAsync(Guid attendanceId);
        Task<ServiceResponse<AttendanceDto>> GetAttendanceByIdAsync(Guid attendanceId);
        Task<ServiceResponse<IEnumerable<AttendanceDto>>> GetAllAttendanceAsync(AttendanceParametersDto queryParameters);
    }
}
