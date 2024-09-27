using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Hall : BaseModel
    {
        public Guid Id { get; set; }
        public Guid RoomTypeId { get; set; }
        public string Name { get; set; }
        public string Capacity { get; set; }
        public string Location { get; set; }
        public bool AvailabilityStatus { get; set; }

        [ForeignKey(nameof(RoomTypeId))]
        public RoomType RoomType { get; set; }
        public ICollection<InventoryItem> Inventories { get; set; }
    }
}