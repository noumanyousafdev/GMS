using GMS.Service.Dtos;
using GMS.Service.Dtos.Halls;
using GMS.Service.Services.Halls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : BaseController
    {
        private readonly IHallService _hallService;
        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HallDto hallDto)
        {
            var response = await _hallService.AddHallAsync(hallDto);
            if (response.Success && response.Data != null && response.Data.Id != Guid.Empty)
            {
                return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
            }
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HallDto hallDto)
        {
            return ReturnFormattedResponse(await _hallService.UpdateHallAsync(hallDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ReturnFormattedResponse(await _hallService.DeleteHallAsync(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return ReturnFormattedResponse(await _hallService.GetHallByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] HallParametersDto queryParameters)
        {
            var response = await _hallService.GetAllHallsAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
