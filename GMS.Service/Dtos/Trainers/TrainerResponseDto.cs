using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Trainers
{
    public class TrainerResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfileImage { get; set; }
        public bool IsActive { get; set; }
        public string Specialization { get; set; }
        public string Schedule { get; set; }
        public int TotalExperience { get; set; }
        public string Shift { get; set; }
    }
}
