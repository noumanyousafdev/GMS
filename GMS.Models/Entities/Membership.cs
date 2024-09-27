using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Membership : BaseModel
    {
        public Guid Id { get; set; }
        public string MemberId { get; set; }
        public Guid PackageId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Payment> Payments { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package Package { get; set; }
        public Member Member { get; set; }
    }
}