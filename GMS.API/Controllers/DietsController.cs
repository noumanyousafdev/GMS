
using GMS.Service.Dtos;
using GMS.Service.Dtos.Diets;
using GMS.Service.Services.Diets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace GMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DietController : BaseController
    {
        private readonly IDietService _dietService;

        public DietController(IDietService dietService)
        {
            _dietService = dietService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDietAsync([FromBody] DietDto dietDto)
        {
            var response = await _dietService.AddDietAsync(dietDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDietAsync(Guid id , [FromBody] DietDto dietDto)
        {
            var response = await _dietService.UpdateDietAsync(id , dietDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDietAsync(Guid id)
        {
            var response = await _dietService.DeleteDietAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietByIdAsync(Guid id)
        {
            var response = await _dietService.GetDietByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDietsAsync([FromQuery] DietParametersDto queryParameters)
        {
            var response = await _dietService.GetAllDietsAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}