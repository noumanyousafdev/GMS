using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Memberships;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.MemberShips;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Memberships
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public MembershipService(ApplicationDbContext dbContext, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<MembershipResponseDto>> GetMembershipByIdAsync(Guid id)
        {
            try
            {
                var membership = await _membershipRepository.FindAsync(id);
                if (membership == null)
                {
                    return ServiceResponse<MembershipResponseDto>.Return404("Membership not found.");
                }

                var membershipDto = _mapper.Map<MembershipResponseDto>(membership);
                return ServiceResponse<MembershipResponseDto>.ReturnResultWith200(membershipDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MembershipResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<MembershipResponseDto>> CreateMembershipAsync(MembershipCreateDto membershipCreateDto)
        {
            try
            {
                var membership = _mapper.Map<Membership>(membershipCreateDto);
                 _membershipRepository.Add(membership);
                await _membershipRepository.SaveAsync();

                var membershipDto = _mapper.Map<MembershipResponseDto>(membership);
                return ServiceResponse<MembershipResponseDto>.ReturnResultWith201(membershipDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MembershipResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<MembershipResponseDto>> UpdateMembershipAsync(Guid id, MembershipCreateDto membershipCreateDto)
        {
            try
            {
                var membership = await _membershipRepository.FindAsync(id);
                if (membership == null)
                {
                    return ServiceResponse<MembershipResponseDto>.Return404("Membership not found.");
                }

                _mapper.Map(membershipCreateDto, membership);
                _membershipRepository.Update(membership);
                await _membershipRepository.SaveAsync();

                var membershipDto = _mapper.Map<MembershipResponseDto>(membership);
                return ServiceResponse<MembershipResponseDto>.ReturnResultWith200(membershipDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MembershipResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteMembershipAsync(Guid id)
        {
            try
            {
                var membership = await _membershipRepository.FindAsync(id);
                if (membership == null)
                {
                    return ServiceResponse<bool>.Return404("Membership not found.");
                }

                _membershipRepository.Remove(membership);
                await _membershipRepository.SaveAsync();

                return ServiceResponse<bool>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<MembershipResponseDto>>> GetAllMembershipsAsync(MembershipParametersDto queryParameters)
        {
            try
            {
                // Start: Retrieve memberships
                IQueryable<Membership> membershipQuery = _dbContext.Memberships; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.MemberId))
                {
                    membershipQuery = membershipQuery.Where(m => m.MemberId == queryParameters.MemberId);
                }

                if (queryParameters.IsActive.HasValue)
                {
                    membershipQuery = membershipQuery.Where(m => m.IsActive == queryParameters.IsActive.Value);
                }

                // Pagination and Sorting
                var totalItems = await membershipQuery.CountAsync(); // Get total count before paging
                var paginatedMemberships = await membershipQuery
                    .ProjectTo<MembershipResponseDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<MembershipResponseDto>>.ReturnResultWith200(paginatedMemberships);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<MembershipResponseDto>>.ReturnException(ex);
            }
        }
    }
}
