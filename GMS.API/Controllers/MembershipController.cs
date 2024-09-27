using GMS.Service.Dtos;
using GMS.Service.Dtos.MemberShips;
using GMS.Service.Services.Memberships;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : BaseController
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMembershipAsync([FromBody] MembershipCreateDto membershipCreateDto)
        {
            var response = await _membershipService.CreateMembershipAsync(membershipCreateDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMembershipAsync(Guid id, [FromBody] MembershipCreateDto membershipCreateDto)
        {
            var response = await _membershipService.UpdateMembershipAsync(id, membershipCreateDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembershipAsync(Guid id)
        {
            var response = await _membershipService.DeleteMembershipAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMembershipByIdAsync(Guid id)
        {
            var response = await _membershipService.GetMembershipByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembershipsAsync([FromQuery] MembershipParametersDto queryParameters)
        {
            var response = await _membershipService.GetAllMembershipsAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
