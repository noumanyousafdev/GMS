using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Member : User, IBaseModel
    {
        public string TrainerId { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public Trainer? Trainer { get; set; }
        public Guid? MembershipId { get; set; }

        [ForeignKey(nameof(MembershipId))]
        public ICollection<Membership> Memberships { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<Measurement> Measurements { get; set; }
        public ICollection<Diet> Diets { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        // Implementing IBaseModel properties
        [JsonIgnore] // Prevents serialization
        public bool IsDeleted { get; set; }
            
        [JsonIgnore] // Prevents serialization
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Default value

        [JsonIgnore] // Prevents serialization
        public DateTime? UpdatedDate { get; set; }

        [JsonIgnore] // Prevents serialization
        public DateTime? DeletedDate { get; set; }
    }
}




