using GMS.Service.Dtos;
using GMS.Service.Dtos.Room;
using GMS.Service.Dtos.Rooms;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Rooms
{
    public interface IRoomTypeService
    {
        Task<ServiceResponse<RoomTypeDto>> AddRoomTypeAsync(RoomTypeDto roomTypeDto);
        Task<ServiceResponse<RoomTypeDto>> UpdateRoomTypeAsync(RoomTypeDto roomTypeDto);
        Task<ServiceResponse<bool>> DeleteRoomTypeAsync(Guid id);
        Task<ServiceResponse<RoomTypeDto>> GetRoomTypeByIdAsync(Guid id);
        Task<ServiceResponse<IEnumerable<RoomTypeDto>>> GetAllRoomTypesAsync(RoomParametersDto queryParameters);
    }
}
