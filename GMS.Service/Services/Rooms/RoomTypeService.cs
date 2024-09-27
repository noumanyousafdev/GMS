using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Room;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Room;
using GMS.Service.Dtos.Rooms;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Dtos.Workout;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Rooms
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public RoomTypeService(ApplicationDbContext dbContext, IRoomTypeRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<RoomTypeDto>> AddRoomTypeAsync(RoomTypeDto roomTypeDto)
        {
            if (await RoomTypeNameExistsAsync(roomTypeDto.Name))
                return ServiceResponse<RoomTypeDto>.Return409("Room type with the same name already exists.");

            try
            {
                var roomType = _mapper.Map<RoomType>(roomTypeDto);
                _repository.Add(roomType);
                await _repository.SaveAsync();

                var createdRoomTypeDto = _mapper.Map<RoomTypeDto>(roomType);
                return ServiceResponse<RoomTypeDto>.ReturnResultWith201(createdRoomTypeDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<RoomTypeDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<RoomTypeDto>> UpdateRoomTypeAsync(RoomTypeDto roomTypeDto)
        {
            var existingRoomType = await _repository.FindAsync(roomTypeDto.Id.GetValueOrDefault());
            if (existingRoomType == null)
                return ServiceResponse<RoomTypeDto>.Return404("Room type not found");

            if (await RoomTypeNameExistsAsync(roomTypeDto.Name, roomTypeDto.Id))
                return ServiceResponse<RoomTypeDto>.Return409("Another room type with the same name already exists.");

            _mapper.Map(roomTypeDto, existingRoomType);
            _repository.Update(existingRoomType);
            await _repository.SaveAsync();

            var updatedRoomTypeDto = _mapper.Map<RoomTypeDto>(existingRoomType);
            return ServiceResponse<RoomTypeDto>.ReturnResultWith200(updatedRoomTypeDto);
        }

        public async Task<ServiceResponse<bool>> DeleteRoomTypeAsync(Guid id)
        {
            var roomType = await _repository.FindAsync(id);
            if (roomType == null)
                return ServiceResponse<bool>.Return404("Room type not found");

            _repository.Remove(roomType);
            await _repository.SaveAsync();

            return ServiceResponse<bool>.ReturnResultWith204();
        }

        public async Task<ServiceResponse<RoomTypeDto>> GetRoomTypeByIdAsync(Guid id)
        {
            var roomType = await _repository.FindAsync(id);
            if (roomType == null)
                return ServiceResponse<RoomTypeDto>.Return404("Room type not found");

            var roomTypeDto = _mapper.Map<RoomTypeDto>(roomType);
            return ServiceResponse<RoomTypeDto>.ReturnResultWith200(roomTypeDto);
        }

        private async Task<bool> RoomTypeNameExistsAsync(string name, Guid? id = null)
        {
            return id.HasValue
                ? await _repository.All.AnyAsync(r => r.Name == name && r.Id != id.Value)
                : await _repository.All.AnyAsync(r => r.Name == name);
        }

        public async Task<ServiceResponse<IEnumerable<RoomTypeDto>>> GetAllRoomTypesAsync(RoomParametersDto queryParameters)
        {
            try
            {
                // Start: Retrieve room types
                IQueryable<RoomType> roomTypeQuery = _dbContext.RoomTypes; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.Name))
                {
                    roomTypeQuery = roomTypeQuery.Where(r => r.Name.Contains(queryParameters.Name));
                }

                // Pagination and Sorting
                var totalItems = await roomTypeQuery.CountAsync(); // Get total count before paging
                var paginatedRoomTypes = await roomTypeQuery
                    .ProjectTo<RoomTypeDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<RoomTypeDto>>.ReturnResultWith200(paginatedRoomTypes);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<RoomTypeDto>>.ReturnException(ex);
            }
        }   
    }
}
