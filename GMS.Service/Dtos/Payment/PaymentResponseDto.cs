    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace GMS.Service.Dtos.Payment
    {
        public class PaymentResponseDto
        {
            public Guid PackageId { get; set; }
            public Guid MembershipId { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal AmountPaid { get; set; }
            public decimal RemainingAmount { get; set; }
            public string PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public string PaymentStatus { get; set; } 
        }
    }
