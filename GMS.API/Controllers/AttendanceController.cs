using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Helper;
using GMS.Service.Services.Attendances;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance([FromBody] AttendanceDto attendanceDto)
        {
            var response = await _attendanceService.CreateAttendanceAsync(attendanceDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance( [FromBody] UpdateAttendance requestDto)
        {
            var response = await _attendanceService.UpdateAttendanceAsync( requestDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(Guid Id)
        {
            var response = await _attendanceService.DeleteAttendanceAsync(Id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(Guid Id)
        {
            var response = await _attendanceService.GetAttendanceByIdAsync(Id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendance([FromQuery] AttendanceParametersDto queryParameters)
        {
            var response = await _attendanceService.GetAllAttendanceAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }

    }
}
