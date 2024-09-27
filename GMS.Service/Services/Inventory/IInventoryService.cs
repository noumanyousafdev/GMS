using GMS.Service.Dtos;
using GMS.Service.Dtos.Inventory;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Inventory
{
    public interface IInventoryService
    {
        Task<ServiceResponse<InventoyItemDto>> CreateInventoryAsync(InventoyItemDto inventoyItemDto);
        Task<ServiceResponse<InventoyItemDto>> UpdateInventoryAsync(Guid id, InventoyItemDto requestDto);
        Task<ServiceResponse<bool>> DeleteInventoryAsync(Guid inventoyItemId);
        Task<ServiceResponse<InventoyItemDto>> GetInventoryByIdAsync(Guid inventoyItemId);
        Task<ServiceResponse<IEnumerable<InventoyItemDto>>> GetAllInventoryAsync(InventoryParametersDto queryParameters);
    }
}
