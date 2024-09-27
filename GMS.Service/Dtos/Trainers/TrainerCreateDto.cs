using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GMS.Service.Dtos.Trainers
{
    public class TrainerCreateDto
    {
        #region Trainer Paramters
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Birth is required.")]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [AllowNull]
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; }

        public string Schedule { get; set; }

        public int TotalExperience { get; set; }

        [Required(ErrorMessage = "Shift is required.")]
        public string Shift { get; set; }
        #endregion 
    }
}
