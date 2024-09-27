using GMS.Models.Entities;
using GMS.Service.Dtos.Login;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Authentication
{
    public interface IAuthService
    {
        Task<ServiceResponse<MemberResponseDto>> RegisterMemberAsync(MemberCreateDto memberDto);
        Task<ServiceResponse<TrainerResponseDto>> RegisterTrainerAsync(TrainerCreateDto trainerDto);
        Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ServiceResponse<string>> CreateJwtTokenAsync(User user, List<string> roles);
    }
}
