using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Workout
{
    public class UpdateWorkout
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Day is required.")]
        [StringLength(20, ErrorMessage = "Day cannot exceed 20 characters.")]
        public string Day { get; set; }

        [Required(ErrorMessage = "Exercise is required.")]
        [StringLength(100, ErrorMessage = "Exercise cannot exceed 100 characters.")]
        public string Exercise { get; set; }

        [Required(ErrorMessage = "Sets are required.")]
        [StringLength(10, ErrorMessage = "Sets cannot exceed 10 characters.")]
        public string Sets { get; set; }

        [Required(ErrorMessage = "Reps are required.")]
        [StringLength(10, ErrorMessage = "Reps cannot exceed 10 characters.")]
        public string Reps { get; set; }
    }
}
