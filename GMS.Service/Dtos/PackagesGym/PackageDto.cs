using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Packages
{
    public class PackageDto
    {
        #region PackageDto Parameters
        public string Name { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }
        public string Duration { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
        #endregion
    }
}
