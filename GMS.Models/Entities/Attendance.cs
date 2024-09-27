using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Attendance : BaseModel
    { 
        #region Attendance Parameters
        public Guid Id { get; set; }
        public string MemberId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }
        #endregion
    }
}
