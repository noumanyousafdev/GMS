using AutoMapper;
using GMS.Models.Entities;
using GMS.Service.Dtos.Login;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<MemberResponseDto>> RegisterMemberAsync(MemberCreateDto memberDto)
        {
            try
            {
                var member = _mapper.Map<Member>(memberDto);
                member.UserName = member.Email; // Set the UserName to the Email address

                var result = await _userManager.CreateAsync(member, memberDto.Password);

                if (!result.Succeeded)
                {
                    return ServiceResponse<MemberResponseDto>.ReturnFailed(400, result.Errors.Select(e => e.Description).ToList());
                }

                if (!await _roleManager.RoleExistsAsync("Member"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Member"));
                }
                await _userManager.AddToRoleAsync(member, "Member");

                var memberDtoResult = _mapper.Map<MemberResponseDto>(member); // Map to MemberResponseDto
                return ServiceResponse<MemberResponseDto>.ReturnResultWith200(memberDtoResult);
            }
            catch (Exception ex)
            {
                return ServiceResponse<MemberResponseDto>.ReturnException(ex);
            }
        }
        public async Task<ServiceResponse<TrainerResponseDto>> RegisterTrainerAsync(TrainerCreateDto trainerDto)
        {
            try
            {
                var trainer = _mapper.Map<Trainer>(trainerDto);
                trainer.UserName = trainer.Email; // Set the UserName to the Email address

                var result = await _userManager.CreateAsync(trainer, trainerDto.Password);

                if (!result.Succeeded)
                {
                    return ServiceResponse<TrainerResponseDto>.ReturnFailed(400, result.Errors.Select(e => e.Description).ToList());
                }

                if (!await _roleManager.RoleExistsAsync("Trainer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Trainer"));
                }
                await _userManager.AddToRoleAsync(trainer, "Trainer");

                var trainerDtoResult = _mapper.Map<TrainerResponseDto>(trainer); // Map to TrainerResponseDto
                return ServiceResponse<TrainerResponseDto>.ReturnResultWith200(trainerDtoResult);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrainerResponseDto>.ReturnException(ex);
            }
        }
        public async Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return ServiceResponse<LoginResponseDto>.ReturnFailed(404, "User not found.");
                }

                var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!result)
                {
                    return ServiceResponse<LoginResponseDto>.ReturnFailed(401, "Invalid password.");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var tokenResult = await CreateJwtTokenAsync(user, roles.ToList());
                if (!tokenResult.Success)
                {
                    return ServiceResponse<LoginResponseDto>.ReturnFailed(tokenResult.StatusCode, tokenResult.Errors);
                }

                return ServiceResponse<LoginResponseDto>.ReturnResultWith200(new LoginResponseDto { JwtToken = tokenResult.Data });
            }
            catch (Exception ex)
            {
                return ServiceResponse<LoginResponseDto>.ReturnException(ex);
            }
        }
        public async Task<ServiceResponse<string>> CreateJwtTokenAsync(User user, List<string> roles)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

                if (user is Member member)
                {
                    claims.Add(new Claim("UserType", "Member"));
                    if (member.TrainerId != null)
                    {
                        claims.Add(new Claim("TrainerId", member.TrainerId));
                    }
                    if (member.MembershipId.HasValue)
                    {
                        claims.Add(new Claim("MembershipId", member.MembershipId.Value.ToString()));
                    }
                }
                else if (user is Trainer trainer)
                {
                    claims.Add(new Claim("UserType", "Trainer"));
                    if (trainer.Specialization != null)
                    {
                        claims.Add(new Claim("Specialization", trainer.Specialization));
                    }
                    claims.Add(new Claim("TotalExperience", trainer.TotalExperience.ToString()));
                }

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return ServiceResponse<string>.ReturnResultWith200(tokenString);
            }
            catch (Exception ex)
            {
                return ServiceResponse<string>.ReturnException(ex);
            }
        }
    }
}
