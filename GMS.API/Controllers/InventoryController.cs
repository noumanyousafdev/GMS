using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Inventory;
using GMS.Service.Services.Attendances;
using GMS.Service.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class InventoryController : BaseController
        {
             private readonly IInventoryService _inventoryService;

            public InventoryController(IInventoryService inventoryService)
            {
                 _inventoryService = inventoryService;
            }

            [HttpPost]
            public async Task<IActionResult> CreateInventory([FromBody] InventoyItemDto inventoyItemDto)
            {
                var response = await _inventoryService.CreateInventoryAsync(inventoyItemDto);
                return ReturnFormattedResponse(response);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateInventory(Guid Id, [FromBody] InventoyItemDto requestDto)
            {
                var response = await _inventoryService.UpdateInventoryAsync(Id, requestDto);
                return ReturnFormattedResponse(response);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteInventory(Guid Id)
            {
                var response = await _inventoryService.DeleteInventoryAsync(Id);
                return ReturnFormattedResponse(response);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetInventoryById(Guid Id)
            {
                var response = await _inventoryService.GetInventoryByIdAsync(Id);
                return ReturnFormattedResponse(response);
            }

            [HttpGet]
            public async Task<IActionResult> GetAllInventory([FromQuery] InventoryParametersDto queryParameters)
            {
                var response = await _inventoryService.GetAllInventoryAsync(queryParameters);
                return ReturnFormattedResponse(response);
            }
    }
}
