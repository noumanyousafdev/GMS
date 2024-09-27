using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Halls;
using GMS.DAL.Repositories.InventoryItems;
using GMS.DAL.Repositories.Room;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Diets;
using GMS.Service.Dtos.Halls;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Halls
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _repository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public HallService(ApplicationDbContext dbContext, IHallRepository repository, IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _roomTypeRepository = roomTypeRepository ?? throw new ArgumentNullException(nameof(roomTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ServiceResponse<HallDto>> GetHallByIdAsync(Guid id)
        {
            var hall = await _repository.FindAsync(id);
            if (hall == null)
                return ServiceResponse<HallDto>.Return404("Hall not found");

            var hallDto = _mapper.Map<HallDto>(hall);
            return ServiceResponse<HallDto>.ReturnResultWith200(hallDto);
        }

        public async Task<ServiceResponse<HallDto>> AddHallAsync(HallDto hallDto)
            {
            // Check if RoomTypeId exists
            if (!await RoomTypeExistsAsync(hallDto.RoomTypeId))
                return ServiceResponse<HallDto>.Return404("RoomType not found");

            if (await HallNameExistsAsync(hallDto.Name))
                return ServiceResponse<HallDto>.Return409("Hall with the same name already exists.");

            try
            {
                var hall = _mapper.Map<Hall>(hallDto);
                _repository.Add(hall);
                await _repository.SaveAsync();

                var createdHallDto = _mapper.Map<HallDto>(hall);
                return ServiceResponse<HallDto>.ReturnResultWith201(createdHallDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<HallDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<HallDto>> UpdateHallAsync(HallDto hallDto)
        {
            // Check if RoomTypeId exists
            if (!await RoomTypeExistsAsync(hallDto.RoomTypeId))
                return ServiceResponse<HallDto>.Return404("RoomType not found");

            var existingHall = await _repository.FindAsync(hallDto.Id);
            if (existingHall == null)
                return ServiceResponse<HallDto>.Return404("Hall not found");

            if (await HallNameExistsAsync(hallDto.Name, hallDto.Id))
                return ServiceResponse<HallDto>.Return409("Another hall with the same name already exists.");

            _mapper.Map(hallDto, existingHall);
            _repository.Update(existingHall);
            await _repository.SaveAsync();

            var updatedHallDto = _mapper.Map<HallDto>(existingHall);
            return ServiceResponse<HallDto>.ReturnResultWith200(updatedHallDto);
        }

        public async Task<ServiceResponse<bool>> DeleteHallAsync(Guid id)
        {
            var hall = await _repository.FindAsync(id);
            if (hall == null)
                return ServiceResponse<bool>.Return404("Hall not found");

            _repository.Remove(hall);
            await _repository.SaveAsync();

            return ServiceResponse<bool>.ReturnResultWith204();
        }

        public async Task<ServiceResponse<IEnumerable<HallDto>>> GetAllHallsAsync(HallParametersDto queryParameters)
        {
            try
            {
                // Access the DbSet directly
                IQueryable<Hall> hallQuery = _dbContext.Halls;

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.Name))
                {
                    hallQuery = hallQuery.Where(h => h.Name.Contains(queryParameters.Name));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Capacity))
                {
                    hallQuery = hallQuery.Where(h => h.Capacity.Contains(queryParameters.Capacity));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Location))
                {
                    hallQuery = hallQuery.Where(h => h.Location.Contains(queryParameters.Location));
                }

                if (queryParameters.AvailabilityStatus.HasValue)
                {
                    hallQuery = hallQuery.Where(h => h.AvailabilityStatus == queryParameters.AvailabilityStatus.Value);
                }
                if(queryParameters.SortBy == "Id")
                {
                    queryParameters.SortBy = "CreatedDate";
                    queryParameters.IsAscending = false;
                }

                // Pagination and Sorting 
                var totalItems = await hallQuery.CountAsync(); // Get total count before paging
                var paginatedHalls = await hallQuery
                    .ProjectTo<HallDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<HallDto>>.ReturnResultWith200(paginatedHalls);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<HallDto>>.ReturnException(ex);
            }
        }

        private async Task<bool> HallNameExistsAsync(string name, Guid? id = null)
        {
            return id.HasValue
                ? await _repository.All.AnyAsync(h => h.Name == name && h.Id != id.Value)
                : await _repository.All.AnyAsync(h => h.Name == name);
        }

        private async Task<bool> RoomTypeExistsAsync(Guid roomTypeId)
        {
            return await _roomTypeRepository.All.AnyAsync(rt => rt.Id == roomTypeId);
        }
    }
}
