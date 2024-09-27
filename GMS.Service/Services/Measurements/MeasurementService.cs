using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Measurements;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Measurements;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Measurements
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;


        public MeasurementService(ApplicationDbContext dbContext, IMeasurementRepository measurementRepository, IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<MeasurementDto>> CreateMeasuremntAsync(MeasurementDto measurementDto)
        {
            try
            {
                var measurementEntity = _mapper.Map<Measurement>(measurementDto);
                _measurementRepository.Add(measurementEntity);
                await _measurementRepository.SaveAsync();

                var createdAddressDto = _mapper.Map<MeasurementDto>(measurementEntity);
                return ServiceResponse<MeasurementDto>.ReturnResultWith201(createdAddressDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MeasurementDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<UpdateMeasurement>> UpdateMeasuremntAsync( UpdateMeasurement requestDto)
        {
            try
            {
                var measurementEntity = await _measurementRepository.FindAsync(requestDto.Id);
                if (measurementEntity == null)
                    return ServiceResponse<UpdateMeasurement>.Return404();

                _mapper.Map(requestDto, measurementEntity);
                _measurementRepository.Update(measurementEntity);
                await _measurementRepository.SaveAsync();

                var updatedAddressDto = _mapper.Map<UpdateMeasurement>(measurementEntity);
                return ServiceResponse<UpdateMeasurement>.ReturnResultWith200(requestDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateMeasurement>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteMeasuremntAsync(Guid measurementId)
        {
            try
            {
                var measurementEntity = await _measurementRepository.FindAsync(measurementId);
                if (measurementEntity == null)
                    return ServiceResponse<bool>.Return404("Address not found");

                _measurementRepository.Remove(measurementEntity);
                await _measurementRepository.SaveAsync();

                return ServiceResponse<bool>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<MeasurementDto>> GetMeasuremntByIdAsync(Guid measurementId)
        {
            try
            {
                var measurementEntity = await _measurementRepository.FindAsync(measurementId);
                if (measurementEntity == null)
                    return ServiceResponse<MeasurementDto>.Return404();

                var workoutDto = _mapper.Map<MeasurementDto>(measurementEntity);
                return ServiceResponse<MeasurementDto>.ReturnResultWith200(workoutDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MeasurementDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<MeasurementDto>>> GetAllMeasuremntAsync(MeasurementParametersDto queryParameters)
        {
            try
            {
                IQueryable<Measurement> measurementQuery = _dbContext.Measurements; // Direct access to DbSet

                // Filtering
                if (queryParameters.Height.HasValue)
                {
                    measurementQuery = measurementQuery.Where(m => m.Height == queryParameters.Height.Value);
                }

                if (queryParameters.Weight.HasValue)
                {
                    measurementQuery = measurementQuery.Where(m => m.Weight == queryParameters.Weight.Value);
                }

                if (queryParameters.BodyFatPercentage.HasValue)
                {
                    measurementQuery = measurementQuery.Where(m => m.BodyFatPercentage == queryParameters.BodyFatPercentage.Value);
                }

                if (queryParameters.MuscleMass.HasValue)
                {
                    measurementQuery = measurementQuery.Where(m => m.MuscleMass == queryParameters.MuscleMass.Value);
                }

                // Pagination and Sorting
                var totalItems = await measurementQuery.CountAsync(); // Get total count before paging
                var paginatedMeasurements = await measurementQuery
                    .ProjectTo<MeasurementDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<MeasurementDto>>.ReturnResultWith200(paginatedMeasurements);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<MeasurementDto>>.ReturnException(ex);
            }
        }
    }
}
