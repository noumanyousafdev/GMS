using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Measurements
{
    public class MeasurementParametersDto : ParametersDto
    {
        public int? Height { get; set; }
        public float? Weight { get; set; }
        public float? BodyFatPercentage { get; set; }
        public float? MuscleMass { get; set; }
    }
}
