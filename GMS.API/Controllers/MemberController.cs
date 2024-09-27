using GMS.API.Hubs;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Services.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;
        private readonly IHubContext<GymHub> _hubContext;


        public MemberController(IMemberService memberService , IHubContext<GymHub> hubContext)
        {
            _memberService = memberService;
            _hubContext = hubContext;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainer(string id, [FromBody] MemberCreateDto memberCreateDto)
        {
            // Call the service method with both id and trainerCreateDto
            var response = await _memberService.UpdateMemberAsync(id, memberCreateDto);

            if (response.Success)
            {
                await _hubContext.Clients.All.SendAsync("MemberUpdated", id); // Send notification to SignalR clients
            }

            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var response = await _memberService.DeleteMemberAsync(id);

            if (response.Success)
            {
                await _hubContext.Clients.All.SendAsync("MemberDeleted", id); // Send notification to SignalR clients
            }

            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(string id)
        {
            var response = await _memberService.GetMemberByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers([FromQuery] MemberParameterDto queryParameters)
        {
            var response = await _memberService.GetAllMembersAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
