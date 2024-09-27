using GMS.Service.Dtos;
using GMS.Service.Dtos.MemberShips;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Memberships
{
    public interface IMembershipService
    {
        Task<ServiceResponse<MembershipResponseDto>> CreateMembershipAsync(MembershipCreateDto membershipCreateDto);
        Task<ServiceResponse<MembershipResponseDto>> UpdateMembershipAsync(Guid id, MembershipCreateDto membershipCreateDto);
        Task<ServiceResponse<bool>> DeleteMembershipAsync(Guid id);
        Task<ServiceResponse<MembershipResponseDto>> GetMembershipByIdAsync(Guid id);
        Task<ServiceResponse<IEnumerable<MembershipResponseDto>>> GetAllMembershipsAsync(MembershipParametersDto queryParameters);
    }
}
