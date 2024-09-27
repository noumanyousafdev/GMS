using GMS.Service.Dtos;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Services.Trainers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : BaseController
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainer(string id, [FromBody] TrainerCreateDto trainerCreateDto)
        {
            // Call the service method with both id and trainerCreateDto
            var response = await _trainerService.UpdateTrainerAsync(id, trainerCreateDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(string id)
        {
            var response = await _trainerService.DeleteTrainerAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainerById(string id)
        {
            var response = await _trainerService.GetTrainerByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainers([FromQuery] TrainerParametersDto queryParameters)
        {
            var response = await _trainerService.GetAllTrainersAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
