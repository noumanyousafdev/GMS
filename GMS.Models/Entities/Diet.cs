using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Models.Entities
{
    public class Diet : BaseModel
    {
        #region Diet Parameters
        public Guid? Id { get; set; }
        public string MemberId { get; set; }
        public string MealType { get; set; }
        public string FoodItem { get; set; }
        public decimal Calories { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }
        #endregion
    }
}
