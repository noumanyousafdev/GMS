using GMS.Service.Dtos;
using GMS.Service.Dtos.Workout;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Workouts
{
    public interface IWorkoutService
    {
        Task<ServiceResponse<WorkoutDto>> CreateWorkoutAsync(WorkoutDto workoutDto);
        Task<ServiceResponse<UpdateWorkout>> UpdateWorkoutAsync(UpdateWorkout requestDto);
        Task<ServiceResponse<bool>> DeleteWorkoutAsync(Guid workoutId);
        Task<ServiceResponse<WorkoutDto>> GetWorkoutByIdAsync(Guid workoutId);
        Task<ServiceResponse<IEnumerable<WorkoutDto>>> GetAllWorkoutAsync(WorkoutParametersDto queryParameters);
    }
}
