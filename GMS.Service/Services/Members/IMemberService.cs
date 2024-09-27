using GMS.Service.Dtos;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Members
{
    public interface IMemberService
    {
        Task<ServiceResponse<MemberResponseDto>> UpdateMemberAsync(string id , MemberCreateDto memberCreateDto);

        Task<ServiceResponse<MemberResponseDto>> DeleteMemberAsync(string id);

        Task<ServiceResponse<MemberResponseDto>> GetMemberByIdAsync(string id);

        Task<ServiceResponse<IEnumerable<MemberResponseDto>>> GetAllMembersAsync(MemberParameterDto queryParameters);
    }
}
