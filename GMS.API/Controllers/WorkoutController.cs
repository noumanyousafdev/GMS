using GMS.Service.Dtos;
using GMS.Service.Dtos.Workout;
using GMS.Service.Services.Workouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class WorkoutController : BaseController
        {
            private readonly IWorkoutService _workoutService;

            public WorkoutController(IWorkoutService workoutService)
            {
                _workoutService = workoutService;
            }

            [HttpPost]

            public async Task<IActionResult> CreateWorkout([FromBody] WorkoutDto workoutDto)
            {
                var response = await _workoutService.CreateWorkoutAsync(workoutDto);
                return ReturnFormattedResponse(response);
            }

            [HttpPut("{id}")]

            public async Task<IActionResult> UpdateWorkout([FromBody] UpdateWorkout requestDto)
            {
                var response = await _workoutService.UpdateWorkoutAsync( requestDto);
                return ReturnFormattedResponse(response);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteWorkout(Guid Id)
            {
                var response = await _workoutService.DeleteWorkoutAsync(Id);
                return ReturnFormattedResponse(response);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetWorkoutById(Guid Id)
            {
                var response = await _workoutService.GetWorkoutByIdAsync(Id);
                return ReturnFormattedResponse(response);
            }

            [HttpGet]

            public async Task<IActionResult> GetAllWorkouts([FromQuery] WorkoutParametersDto queryParameters)
        {
            var response = await _workoutService.GetAllWorkoutAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}

