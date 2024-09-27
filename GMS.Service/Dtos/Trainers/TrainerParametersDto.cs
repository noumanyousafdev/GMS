using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Trainers
{
    public class TrainerParametersDto : ParametersDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public int? TotalExperience { get; set; }
        public string? Shift { get; set; }
    }
}
