using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Diets;
using GMS.DAL.Repositories.Members;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Diets;
using GMS.Service.Dtos.Packages;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Diets
{
    public class DietService : IDietService
    {
        private readonly IDietRepository _dietRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DietService(ApplicationDbContext dbContext, IDietRepository dietRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _dietRepository = dietRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<DietDto>> AddDietAsync(DietDto Dto)
        {

            var dietDomain = _mapper.Map<Diet>(Dto);
            _dietRepository.Add(dietDomain);
            await _dietRepository.SaveAsync();

            var createdDietDto = _mapper.Map<DietDto>(dietDomain);
            return ServiceResponse<DietDto>.ReturnResultWith201(createdDietDto);




        }

        public async Task<ServiceResponse<bool>> DeleteDietAsync(Guid id)
        {
            var dietDomain = await _dietRepository.FindAsync(id);
            if (dietDomain == null)
            {
                return ServiceResponse<bool>.Return404("Diet not Found");
            }

            _dietRepository.Delete(dietDomain);
            await _dietRepository.SaveAsync();

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }

        public async Task<ServiceResponse<DietDto>> GetDietByIdAsync(Guid id)
        {
            // Domain Model from Database 
            var dietDomain = await _dietRepository.FindAsync(id);
            if (dietDomain == null)
            {
                return ServiceResponse<DietDto>.Return404("Diet not Found");

            }
            // Domain Model to Dto
            var dietDto = _mapper.Map<DietDto>(dietDomain);

            // Return Back to Client
            return ServiceResponse<DietDto>.ReturnResultWith200(dietDto);


        }

        public async Task<ServiceResponse<DietDto>> UpdateDietAsync(Guid id, DietDto dietDto)
        {
            // Find the existing domain model by ID
            var existingDomainModel = await _dietRepository.FindAsync(id);

            // Check if the domain model exists
            if (existingDomainModel == null)
            {
                return ServiceResponse<DietDto>.Return404("Not Found");
            }

            // Map changes from DTO to the existing domain model
            _mapper.Map(dietDto, existingDomainModel);

            // Update changes in the database
            _dietRepository.Update(existingDomainModel);
            await _dietRepository.SaveAsync();

            // Map the updated domain model to DTO
            var updatedDietDto = _mapper.Map<DietDto>(existingDomainModel);
            return ServiceResponse<DietDto>.ReturnResultWith200(updatedDietDto);
        }

        public async Task<ServiceResponse<IEnumerable<DietDto>>> GetAllDietsAsync(DietParametersDto queryParameters)
        {
            try
            {
                IQueryable<Diet> dietQuery = _dbContext.Diets; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrEmpty(queryParameters.MealType))
                {
                    dietQuery = dietQuery.Where(x => x.MealType.Contains(queryParameters.MealType));
                }
                if (!string.IsNullOrEmpty(queryParameters.FoodItem))
                {
                    dietQuery = dietQuery.Where(x => x.FoodItem.Contains(queryParameters.FoodItem));
                }
                if (queryParameters.Calories.HasValue)
                {
                    dietQuery = dietQuery.Where(x => x.Calories == queryParameters.Calories.Value);
                }


                // Pagination and Sorting
                var totalItems = await dietQuery.CountAsync(); // Get total count before paging
                var paginatedDiets = await dietQuery
                    .ProjectTo<DietDto>(_mapper.ConfigurationProvider)
                    .OrderByCustom(queryParameters.SortBy,queryParameters.IsAscending)
                    .Paginate(queryParameters.PageNumber,queryParameters.PageSize)
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<DietDto>>.ReturnResultWith200(paginatedDiets);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<DietDto>>.ReturnException(ex);
            }
        }
    }
}