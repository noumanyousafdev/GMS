using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.InventoryItems;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Feedback;
using GMS.Service.Dtos.Halls;
using GMS.Service.Dtos.Inventory;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryItemRepository _InventoryRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public InventoryService(ApplicationDbContext dbContext, IInventoryItemRepository InventoryRepository, IMapper mapper)
        {
            _InventoryRepository = InventoryRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<InventoyItemDto>> CreateInventoryAsync(InventoyItemDto inventoyItemDto)
        {
            try
            {
                var InventoyEntity = _mapper.Map<InventoryItem>(inventoyItemDto);
                _InventoryRepository.Add(InventoyEntity);
                await _InventoryRepository.SaveAsync();

                var createdAddressDto = _mapper.Map<InventoyItemDto>(InventoyEntity);
                return ServiceResponse<InventoyItemDto>.ReturnResultWith201(createdAddressDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<InventoyItemDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<InventoyItemDto>> UpdateInventoryAsync(Guid Id , InventoyItemDto requestDto)
        {
            try
            {
                var addressEntity = await _InventoryRepository.FindAsync(Id);
                if (addressEntity == null)
                    return ServiceResponse<InventoyItemDto>.Return404();

                _mapper.Map(requestDto, addressEntity);
                _InventoryRepository.Update(addressEntity);
                await _InventoryRepository.SaveAsync();

                var updatedAddressDto = _mapper.Map<InventoyItemDto>(addressEntity);
                return ServiceResponse<InventoyItemDto>.ReturnResultWith200(requestDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<InventoyItemDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteInventoryAsync(Guid inventoyId)
        {
            try
            {
                var workoutEntity = await _InventoryRepository.FindAsync(inventoyId);
                if (workoutEntity == null)
                    return ServiceResponse<bool>.Return404("Address not found");

                _InventoryRepository.Remove(workoutEntity);
                await _InventoryRepository.SaveAsync();

                return ServiceResponse<bool>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<InventoyItemDto>> GetInventoryByIdAsync(Guid inventoyId)
        {
            try
            {
                var inventoyEntity = await _InventoryRepository.FindAsync(inventoyId);
                if (inventoyEntity == null)
                    return ServiceResponse<InventoyItemDto>.Return404();

                var workoutDto = _mapper.Map<InventoyItemDto>(inventoyEntity);
                return ServiceResponse<InventoyItemDto>.ReturnResultWith200(workoutDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<InventoyItemDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<InventoyItemDto>>> GetAllInventoryAsync(InventoryParametersDto queryParameters)
        {
            try
            {
                IQueryable<InventoryItem> inventoryQuery = _dbContext.InventoryItems; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.ItemName))
                {
                    inventoryQuery = inventoryQuery.Where(i => i.ItemName.Contains(queryParameters.ItemName));
                }
                if (queryParameters.LastMaintenanceDate.HasValue)
                {
                    inventoryQuery = inventoryQuery.Where(i => i.LastMaintenanceDate.Date == queryParameters.LastMaintenanceDate.Value.Date);
                }
                if (queryParameters.NextMaintenanceDate.HasValue)
                {
                    inventoryQuery = inventoryQuery.Where(i => i.NextMaintenanceDate.Date == queryParameters.NextMaintenanceDate.Value.Date);
                }

                // Pagination and Sorting
                var totalItems = await inventoryQuery.CountAsync(); // Get total count before paging
                var paginatedInventory = await inventoryQuery
                    .ProjectTo<InventoyItemDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<InventoyItemDto>>.ReturnResultWith200(paginatedInventory);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<InventoyItemDto>>.ReturnException(ex);
            }
        }
    }
}
