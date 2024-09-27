using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Measurement : BaseModel
    { 
        #region Measurement Parameters
        public Guid Id { get; set; }
        public string MemberId { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public float BodyFatPercentage { get; set; }
        public float MuscleMass { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }
        #endregion    
    }
}
