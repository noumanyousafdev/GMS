using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Halls
{
    public class HallParametersDto : ParametersDto
    {
        public string? Name { get; set; }
        public string? Capacity { get; set; }
        public string? Location { get; set; }
        public bool? AvailabilityStatus { get; set; }
    }
}
