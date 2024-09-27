using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Payment : BaseModel
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public Guid MembershipId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingAmount => TotalAmount - AmountPaid; 
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } //Paid, Partial, Declined 

        [ForeignKey(nameof(MembershipId))]
        public Membership Membership { get; set; }  // One Payment to one Membership
        public Package Package { get; set; }        // One Payment to one Package
    }
}



