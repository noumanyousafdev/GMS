using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Measurements
{
    public class UpdateMeasurement
    {
        public Guid Id { get; set; }

        [Required]
        public string MemberId { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public float BodyFatPercentage { get; set; }
        public float MuscleMass { get; set; }
    }
}
