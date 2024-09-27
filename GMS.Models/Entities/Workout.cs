using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Workout : BaseModel
    {
        #region Workout Parameters
        public Guid Id { get; set; }
        public string MemberId { get; set; }
        public string Day { get; set; }
        public string Exercise { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }
        #endregion
    }
}
