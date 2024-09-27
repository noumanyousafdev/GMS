using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Measurements;
using GMS.Service.Services.Attendances;
using GMS.Service.Services.Measurements;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : BaseController
    {
        private readonly IMeasurementService _measurementService;
        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementDto measurementDto)
        {
            var response = await _measurementService.CreateMeasuremntAsync(measurementDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement([FromBody] UpdateMeasurement requestDto)
        {
            var response = await _measurementService.UpdateMeasuremntAsync(requestDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(Guid Id)
        {
            var response = await _measurementService.DeleteMeasuremntAsync(Id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurementById(Guid Id)
        {
            var response = await _measurementService.GetMeasuremntByIdAsync(Id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeasurements([FromQuery] MeasurementParametersDto queryParameters)
        {
            var response = await _measurementService.GetAllMeasuremntAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}

