using GMS.Service.Dtos;
using GMS.Service.Dtos.Room;
using GMS.Service.Dtos.Rooms;
using GMS.Service.Services.Rooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : BaseController
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoomTypeDto roomTypeDto)
        {
            var response = await _roomTypeService.AddRoomTypeAsync(roomTypeDto);
            if (response.Success && response.Data != null && response.Data.Id != Guid.Empty)
            {
                return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
            }
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RoomTypeDto roomTypeDto)
        {
            var response = await _roomTypeService.UpdateRoomTypeAsync(roomTypeDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _roomTypeService.DeleteRoomTypeAsync(id);
            return ReturnFormattedResponse(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _roomTypeService.GetRoomTypeByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RoomParametersDto queryParameters)
        {
            var response = await _roomTypeService.GetAllRoomTypesAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}

