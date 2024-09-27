using GMS.Models.Entities;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.Packages;
using GMS.Service.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.MemberShips
{
    public class MembershipCreateDto
    {
        public string MemberId { get; set; }
        public Guid? PackageId { get; set; }
        public bool IsActive { get; set; } 
    }
}


