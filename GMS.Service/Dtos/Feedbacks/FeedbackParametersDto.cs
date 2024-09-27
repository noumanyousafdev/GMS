using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Feedbacks
{
    public class FeedbackParametersDto : ParametersDto
    {
        public DateTime? Date { get; set; }
        public string? FeedbackType { get; set; }
        public string? Description { get; set; }
        public string? ResolutionStatus { get; set; }
    }
}
