using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Workouts;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Workout;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Workouts
{
        public class WorkoutService : IWorkoutService
        {
            private readonly IWorkoutRepository _workoutRepository;
            private readonly IMapper _mapper;
            private readonly ApplicationDbContext _dbContext;

            public WorkoutService(ApplicationDbContext dbContext, IWorkoutRepository workoutRepository, IMapper mapper)
            {
                _workoutRepository = workoutRepository;
                _mapper = mapper;
                _dbContext = dbContext;
            }

            public async Task<ServiceResponse<WorkoutDto>> CreateWorkoutAsync(WorkoutDto workoutDto)
            {
                try
                {
                    var addressEntity = _mapper.Map<Workout>(workoutDto);
                    _workoutRepository.Add(addressEntity);
                    await _workoutRepository.SaveAsync();

                    var createdAddressDto = _mapper.Map<WorkoutDto>(addressEntity);
                    return ServiceResponse<WorkoutDto>.ReturnResultWith201(createdAddressDto);
                }
                catch (Exception ex)
                {
                    return ServiceResponse<WorkoutDto>.ReturnException(ex);
                }
            }

            public async Task<ServiceResponse<UpdateWorkout>> UpdateWorkoutAsync( UpdateWorkout requestDto)
            {
                try
                {
                    var addressEntity = await _workoutRepository.FindAsync(requestDto.Id);
                    if (addressEntity == null)
                        return ServiceResponse<UpdateWorkout>.Return404();

                    _mapper.Map(requestDto, addressEntity);
                    _workoutRepository.Update(addressEntity);
                    await _workoutRepository.SaveAsync();

                    var updatedAddressDto = _mapper.Map<UpdateWorkout>(addressEntity);
                    return ServiceResponse<UpdateWorkout>.ReturnResultWith200(requestDto);
                }
                catch (Exception ex)
                {
                    return ServiceResponse<UpdateWorkout>.ReturnException(ex);
                }
            }

            public async Task<ServiceResponse<bool>> DeleteWorkoutAsync(Guid workoutId)
            {
                try
                {
                    var workoutEntity = await _workoutRepository.FindAsync(workoutId);
                    if (workoutEntity == null)
                        return ServiceResponse<bool>.Return404("Address not found");

                    _workoutRepository.Remove(workoutEntity);
                    await _workoutRepository.SaveAsync();

                    return ServiceResponse<bool>.ReturnResultWith204();
                }
                catch (Exception ex)
                {
                    return ServiceResponse<bool>.ReturnException(ex);
                }
            }

            public async Task<ServiceResponse<WorkoutDto>> GetWorkoutByIdAsync(Guid workoutId)
            {
                try
                {
                    var workoutEntity = await _workoutRepository.FindAsync(workoutId);
                    if (workoutEntity == null)
                        return ServiceResponse<WorkoutDto>.Return404();

                    var workoutDto = _mapper.Map<WorkoutDto>(workoutEntity);
                    return ServiceResponse<WorkoutDto>.ReturnResultWith200(workoutDto);
                }
                catch (Exception ex)
                {
                    return ServiceResponse<WorkoutDto>.ReturnException(ex);
                }
            }

            public async Task<ServiceResponse<IEnumerable<WorkoutDto>>> GetAllWorkoutAsync(WorkoutParametersDto queryParameters)
            {
            try
            {
                // Start: Retrieve workouts
                IQueryable<Workout> workoutQuery = _dbContext.Workouts; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.Day))
                {
                    workoutQuery = workoutQuery.Where(w => w.Day.Contains(queryParameters.Day));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Exercise))
                {
                    workoutQuery = workoutQuery.Where(w => w.Exercise.Contains(queryParameters.Exercise));
                }

                // Pagination and Sorting
                var totalItems = await workoutQuery.CountAsync(); // Get total count before paging
                var paginatedWorkouts = await workoutQuery
                    .ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<WorkoutDto>>.ReturnResultWith200(paginatedWorkouts);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<WorkoutDto>>.ReturnException(ex);
            }
        }
        }
}
