using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Payment
{
    public class PaymentCreateDto
    {
        public Guid PackageId { get; set; }
        public Guid MembershipId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingAmount { get; set; }
        public string PaymentMethod { get; set; }

        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } 
    }
}
