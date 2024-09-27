using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Attendances
{
    public class UpdateAttendance
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Check-in time is required.")]
        public DateTime CheckInTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Check-out time is required.")]
        public DateTime CheckOutTime { get; set; } = DateTime.Now.AddHours(8);
    }
}
