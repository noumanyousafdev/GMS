using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Room
{
    public class RoomTypeDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Room name is required.")]
        [MaxLength(100, ErrorMessage = "Room name can't exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; }
    }
}
