using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.MemberShips
{
    public class MembershipParametersDto : ParametersDto
    {
        public string? MemberId { get; set; }
        public bool? IsActive { get; set; }
    }
}
