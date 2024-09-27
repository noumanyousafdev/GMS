using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Feedback : BaseModel
    {
        #region Feedback Parameters
        public Guid Id { get; set; }
        public string MemberId { get; set; }
        public DateTime Date { get; set; }
        public string FeedbackType { get; set; }
        public string Description { get; set; }
        public string ResolutionStatus { get; set; }
        public Member Member { get; set; }
        #endregion
    }
}
