using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Feedback
{
    public class FeedbackDto
    {

        [Required(ErrorMessage = "Member ID is required.")]
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Feedback type is required.")]
        [StringLength(50, ErrorMessage = "Feedback type cannot exceed 50 characters.")]
        public string FeedbackType { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Resolution status is required.")]
        [StringLength(50, ErrorMessage = "Resolution status cannot exceed 50 characters.")]
        public string ResolutionStatus { get; set; }
    }
}
