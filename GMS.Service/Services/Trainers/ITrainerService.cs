using GMS.Service.Dtos;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Trainers
{
    public interface ITrainerService
    {
        Task<ServiceResponse<TrainerResponseDto>>GetTrainerByIdAsync (string id);
        Task<ServiceResponse<TrainerResponseDto>> UpdateTrainerAsync (string id , TrainerCreateDto trainerCreateDto);   
        Task<ServiceResponse<TrainerResponseDto>> DeleteTrainerAsync (string id);
        Task<ServiceResponse<IEnumerable<TrainerResponseDto>>> GetAllTrainersAsync(TrainerParametersDto queryParameters);
    }
}
