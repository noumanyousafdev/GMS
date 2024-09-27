using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Workout
{
    public class WorkoutParametersDto : ParametersDto
    {
        public string? Day { get; set; }
        public string? Exercise { get; set; }
    }
}
