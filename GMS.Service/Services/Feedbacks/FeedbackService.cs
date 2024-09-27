using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Attendances;
using GMS.DAL.Repositories.Feedbacks;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Feedback;
using GMS.Service.Dtos.Feedbacks;
using GMS.Service.Dtos.Halls;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {

        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public FeedbackService(ApplicationDbContext dbContext, IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FeedbackDto>> CreateFeedbackAsync(FeedbackDto feedbackDto)
        {
            try
            {
                var addressEntity = _mapper.Map<Feedback>(feedbackDto);
                _feedbackRepository.Add(addressEntity);
                await _feedbackRepository.SaveAsync();

                var createdfeedbackDto = _mapper.Map<FeedbackDto>(addressEntity);
                return ServiceResponse<FeedbackDto>.ReturnResultWith201(createdfeedbackDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<FeedbackDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<UpdateFeedback>> UpdateFeedbackAsync(UpdateFeedback requestDto)
        {
            try
            {
                var addressEntity = await _feedbackRepository.FindAsync(requestDto.Id);
                if (addressEntity == null)
                    return ServiceResponse<UpdateFeedback>.Return404();

                _mapper.Map(requestDto, addressEntity);
                _feedbackRepository.Update(addressEntity);
                await _feedbackRepository.SaveAsync();

                var updatedAddressDto = _mapper.Map<UpdateFeedback>(addressEntity);
                return ServiceResponse<UpdateFeedback>.ReturnResultWith200(requestDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateFeedback>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteFeedbackAsync(Guid feedbackId)
        {
            try
            {
                var addressEntity = await _feedbackRepository.FindAsync(feedbackId);
                if (addressEntity == null)
                    return ServiceResponse<bool>.Return404("Address not found");

                _feedbackRepository.Remove(addressEntity);
                await _feedbackRepository.SaveAsync();

                return ServiceResponse<bool>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<FeedbackDto>> GetFeedbackByIdAsync(Guid feedbackId)
        {
            try
            {
                var feedbackEntity = await _feedbackRepository.FindAsync(feedbackId);
                if (feedbackEntity == null)
                    return ServiceResponse<FeedbackDto>.Return404();

                var feedbackDto = _mapper.Map<FeedbackDto>(feedbackEntity);
                return ServiceResponse<FeedbackDto>.ReturnResultWith200(feedbackDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<FeedbackDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<FeedbackDto>>> GetAllFeedbackAsync(FeedbackParametersDto queryParameters)
        {
            try
            {
                IQueryable<Feedback> feedbackQuery = _dbContext.Feedbacks; // Direct access to DbSet

                // Filtering
                if (queryParameters.Date.HasValue)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.Date.Date == queryParameters.Date.Value.Date);
                }

                if (!string.IsNullOrEmpty(queryParameters.FeedbackType))
                {
                    feedbackQuery = feedbackQuery.Where(x => x.FeedbackType.Contains(queryParameters.FeedbackType));
                }

                if (!string.IsNullOrEmpty(queryParameters.Description))
                {
                    feedbackQuery = feedbackQuery.Where(x => x.Description.Contains(queryParameters.Description));
                }

                if (!string.IsNullOrEmpty(queryParameters.ResolutionStatus))
                {
                    feedbackQuery = feedbackQuery.Where(x => x.ResolutionStatus.Contains(queryParameters.ResolutionStatus));
                }

                // Pagination and Sorting 
                var totalItems = await feedbackQuery.CountAsync(); // Get total count before paging
                var paginatedFeedbacks = await feedbackQuery
                    .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<FeedbackDto>>.ReturnResultWith200(paginatedFeedbacks);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<FeedbackDto>>.ReturnException(ex);
            }
        }
    }
}

