using GMS.API.Hubs;
using GMS.Service.Dtos.Login;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IHubContext<GymHub> _hubContext;

        public AuthController(IAuthService authService, IHubContext<GymHub> hubContext)
        {
            _authService = authService;
            _hubContext = hubContext;
        }

        [HttpPost("register/member")]
        public async Task<IActionResult> RegisterMemberAsync([FromBody] MemberCreateDto memberDto)
        {

            var response = await _authService.RegisterMemberAsync(memberDto);


            if (response.Success)
            {
                try
                {
                    await _hubContext.Clients.All.SendAsync("MemberAdded", response.Data.Id);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error notifying clients: {ex.Message}");
                }
            }

            return ReturnFormattedResponse(response);
        }

        [HttpPost("register/trainer")]
        public async Task<IActionResult> RegisterTrainerAsync([FromBody] TrainerCreateDto trainerDto)
        {
            var response = await _authService.RegisterTrainerAsync(trainerDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return ReturnFormattedResponse(response);
        }
    }
}
