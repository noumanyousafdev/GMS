using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Members;
using GMS.Data;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Measurements;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Members
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public MemberService(ApplicationDbContext dbContext, IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<MemberResponseDto>> GetMemberByIdAsync(string id)
        {
            try
            {
                var member = await _memberRepository.FindAsync(id);
                if (member == null)
                {
                    return ServiceResponse<MemberResponseDto>.Return404("Member not found.");
                }

                var memberDto = _mapper.Map<MemberResponseDto>(member);
                return ServiceResponse<MemberResponseDto>.ReturnResultWith200(memberDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MemberResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<MemberResponseDto>> UpdateMemberAsync(string id, MemberCreateDto memberCreateDto)
        {
            try
            {
                // Find the trainer by the provided id
                var member = await _memberRepository.FindAsync(id);
                if (member == null)
                {
                    return ServiceResponse<MemberResponseDto>.Return404("Member not found.");
                }

                // Map the updated properties from trainerCreateDto to the found trainer
                _mapper.Map(memberCreateDto, member);

                // Update the trainer in the repository
                _memberRepository.Update(member);
                await _memberRepository.SaveAsync();

                // Map the updated member to a response DTO
                var memberDto = _mapper.Map<MemberResponseDto>(member);
                return ServiceResponse<MemberResponseDto>.ReturnResultWith200(memberDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MemberResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<MemberResponseDto>> DeleteMemberAsync(string id)
        {
            try
            {
                var member = await _memberRepository.FindAsync(id);
                if (member == null)
                {
                    return ServiceResponse<MemberResponseDto>.Return404("Member not found.");
                }

                _memberRepository.Remove(member);
                await _memberRepository.SaveAsync();

                return ServiceResponse<MemberResponseDto>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<MemberResponseDto>.ReturnException(ex);
            }
        }
        
        public async Task<ServiceResponse<IEnumerable<MemberResponseDto>>> GetAllMembersAsync(MemberParameterDto queryParameters)
        {
            try
            {
                // Start: Retrieve members
                IQueryable<Member> memberQuery = _dbContext.Members; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.FirstName))
                {
                    memberQuery = memberQuery.Where(m => m.FirstName.Contains(queryParameters.FirstName));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.LastName))
                {
                    memberQuery = memberQuery.Where(m => m.LastName.Contains(queryParameters.LastName));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Gender))
                {
                    memberQuery = memberQuery.Where(m => m.Gender == queryParameters.Gender);
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Address))
                {
                    memberQuery = memberQuery.Where(m => m.Address.Contains(queryParameters.Address));
                }

                if (queryParameters.IsActive.HasValue)
                {
                    memberQuery = memberQuery.Where(m => m.IsActive == queryParameters.IsActive.Value);
                }

                // Pagination and Sorting
                var totalItems = await memberQuery.CountAsync(); // Get total count before paging
                var paginatedMembers = await memberQuery
                    .ProjectTo<MemberResponseDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                .OrderByCustom(queryParameters.SortBy ?? "Member_CreatedDate", queryParameters.IsAscending) // Default to CreatedDate desc (false for descending)

                    //.OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<MemberResponseDto>>.ReturnResultWith200(paginatedMembers);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<MemberResponseDto>>.ReturnException(ex);
            }
        }
    }
}
