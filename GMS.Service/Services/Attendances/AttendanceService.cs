using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Attendances;
using GMS.DAL.Repositories.Workouts;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Workout;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Attendances
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AttendanceService(ApplicationDbContext dbContext, IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AttendanceDto>> CreateAttendanceAsync(AttendanceDto attendanceDto)
        {
            try
            {
                var addressEntity = _mapper.Map<Attendance>(attendanceDto);
                _attendanceRepository.Add(addressEntity);
                await _attendanceRepository.SaveAsync();

                var createdAttendanceDto = _mapper.Map<AttendanceDto>(addressEntity);
                return ServiceResponse<AttendanceDto>.ReturnResultWith201(createdAttendanceDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<AttendanceDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<UpdateAttendance>> UpdateAttendanceAsync( UpdateAttendance requestDto)
        {
            try
            {
                var addressEntity = await _attendanceRepository.FindAsync(requestDto.Id);
                if (addressEntity == null)
                    return ServiceResponse<UpdateAttendance>.Return404();

                _mapper.Map(requestDto, addressEntity);
                _attendanceRepository.Update(addressEntity);
                await _attendanceRepository.SaveAsync();

                var updatedAddressDto = _mapper.Map<UpdateAttendance>(addressEntity);
                return ServiceResponse<UpdateAttendance>.ReturnResultWith200(requestDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateAttendance>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAttendanceAsync(Guid attendanceId)
        {
            try
            {
                var addressEntity = await _attendanceRepository.FindAsync(attendanceId);
                if (addressEntity == null)
                    return ServiceResponse<bool>.Return404("Address not found");

                _attendanceRepository.Remove(addressEntity);
                await _attendanceRepository.SaveAsync();

                return ServiceResponse<bool>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<AttendanceDto>> GetAttendanceByIdAsync(Guid attendanceId)
        {
            try
            {
                var addressEntity = await _attendanceRepository.FindAsync(attendanceId);
                if (addressEntity == null)
                    return ServiceResponse<AttendanceDto>.Return404();

                var addressDto = _mapper.Map<AttendanceDto>(addressEntity);
                return ServiceResponse<AttendanceDto>.ReturnResultWith200(addressDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<AttendanceDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<AttendanceDto>>> GetAllAttendanceAsync(AttendanceParametersDto queryParameters)
        {
            try
            {
                IQueryable<Attendance> attendanceQuery = _dbContext.Attendances; // Direct access to DbSet

                // Filtering
                if (queryParameters.Date.HasValue)
                {
                    attendanceQuery = attendanceQuery.Where(x => x.Date.Date == queryParameters.Date.Value.Date);
                }
                if (queryParameters.CheckInTime.HasValue)
                {
                    attendanceQuery = attendanceQuery.Where(x => x.CheckInTime >= queryParameters.CheckInTime.Value);
                }
                if (queryParameters.CheckOutTime.HasValue)
                {
                    attendanceQuery = attendanceQuery.Where(x => x.CheckOutTime <= queryParameters.CheckOutTime.Value);
                }

                // Pagination and Sorting 
                var totalItems = await attendanceQuery.CountAsync(); // Get total count before paging
                var paginatedAttendances = await attendanceQuery
                    .ProjectTo<AttendanceDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<AttendanceDto>>.ReturnResultWith200(paginatedAttendances);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<AttendanceDto>>.ReturnException(ex);
            }
        }
    }
}

