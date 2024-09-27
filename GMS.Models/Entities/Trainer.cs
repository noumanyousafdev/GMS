using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Trainer : User , IBaseModel
    {
        public string Specialization { get; set; }
        public string Schedule { get; set; }
        public int TotalExperience { get; set; }
        public string Shift { get; set; } // Day, Night
        public ICollection<Member> Members { get; set; }

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
