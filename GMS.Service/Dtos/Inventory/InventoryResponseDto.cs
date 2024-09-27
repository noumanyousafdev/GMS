using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Inventory
{
    public class InventoryResponseDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "HallId is required.")]
        public Guid HallId { get; set; }

        [Required(ErrorMessage = "ItemName is required.")]
        [StringLength(100, ErrorMessage = "ItemName cannot exceed 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Maintenance Schedule is required.")]
        [StringLength(50, ErrorMessage = "Maintenance Schedule cannot exceed 50 characters.")]
        public string MaintainanceSchedule { get; set; }

        [Required(ErrorMessage = "Last Maintenance Date is required.")]
        public DateTime LastMaintenanceDate { get; set; }

        [Required(ErrorMessage = "Next Maintenance Date is required.")]
        public DateTime NextMaintenanceDate { get; set; }
    }
}
