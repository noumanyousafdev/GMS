using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Trainers;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Trainers
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public TrainerService(ApplicationDbContext dbContext, ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<TrainerResponseDto>> GetTrainerByIdAsync(string id)
        {
            try
            {
                var trainer = await _trainerRepository.FindAsync((id));
                if (trainer == null)
                {
                    return ServiceResponse<TrainerResponseDto>.Return404("Trainer not found.");
                }

                var trainerDto = _mapper.Map<TrainerResponseDto>(trainer);
                return ServiceResponse<TrainerResponseDto>.ReturnResultWith200(trainerDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrainerResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<TrainerResponseDto>> UpdateTrainerAsync(string id, TrainerCreateDto trainerCreateDto)
        {
            try
            {
                // Find the trainer by the provided id
                var trainer = await _trainerRepository.FindAsync(id);
                if (trainer == null)
                {
                    return ServiceResponse<TrainerResponseDto>.Return404("Trainer not found.");
                }

                // Map the updated properties from trainerCreateDto to the found trainer
                _mapper.Map(trainerCreateDto, trainer);

                // Update the trainer in the repository
                _trainerRepository.Update(trainer);
                await _trainerRepository.SaveAsync();

                // Map the updated trainer to a response DTO
                var trainerDto = _mapper.Map<TrainerResponseDto>(trainer);
                return ServiceResponse<TrainerResponseDto>.ReturnResultWith200(trainerDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrainerResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<TrainerResponseDto>> DeleteTrainerAsync(string id)
        {
            try
            {
                var trainer = await _trainerRepository.FindAsync((id));
                if (trainer == null)
                {
                    return ServiceResponse<TrainerResponseDto>.Return404("Trainer not found.");
                }

                _trainerRepository.Remove(trainer);
                await _trainerRepository.SaveAsync();

                return ServiceResponse<TrainerResponseDto>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrainerResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<TrainerResponseDto>>> GetAllTrainersAsync(TrainerParametersDto queryParameters)
        {
            try
            {
                // Start: Retrieve trainers
                IQueryable<Trainer> trainerQuery = _dbContext.Trainers; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.FirstName))
                {
                    trainerQuery = trainerQuery.Where(t => t.FirstName.Contains(queryParameters.FirstName));
                }
                if (!string.IsNullOrWhiteSpace(queryParameters.LastName))
                {
                    trainerQuery = trainerQuery.Where(t => t.LastName.Contains(queryParameters.LastName));
                }
                if (!string.IsNullOrWhiteSpace(queryParameters.Gender))
                {
                    trainerQuery = trainerQuery.Where(t => t.Gender == queryParameters.Gender);
                }
                if (queryParameters.IsActive.HasValue)
                {
                    trainerQuery = trainerQuery.Where(t => t.IsActive == queryParameters.IsActive.Value);
                }
                if (queryParameters.TotalExperience.HasValue)
                {
                    trainerQuery = trainerQuery.Where(t => t.TotalExperience == queryParameters.TotalExperience.Value);
                }
                if (!string.IsNullOrWhiteSpace(queryParameters.Shift))
                {
                    trainerQuery = trainerQuery.Where(t => t.Shift == queryParameters.Shift);
                }

                // Pagination and Sorting
                var totalItems = await trainerQuery.CountAsync(); // Get total count before paging
                var paginatedTrainers = await trainerQuery
                    .ProjectTo<TrainerResponseDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<TrainerResponseDto>>.ReturnResultWith200(paginatedTrainers);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<TrainerResponseDto>>.ReturnException(ex);
            }
        }
    }
}
