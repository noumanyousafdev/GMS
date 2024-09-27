using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class InventoryItem : BaseModel
    {
        #region Inventory Parameters
        public Guid Id { get; set; }
        public Guid HallId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string MaintainanceSchedule { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public DateTime NextMaintenanceDate { get; set; }

        [ForeignKey(nameof(HallId))]
        public Hall? Hall { get; set; }
        #endregion
    }
}


