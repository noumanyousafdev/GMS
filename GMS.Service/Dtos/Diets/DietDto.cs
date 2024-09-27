using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Diets
{
    public class DietDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        public string MemberId { get; set; }
        public string MealType { get; set; }
        public string FoodItem { get; set; }
        public decimal Calories { get; set; }
    }
}
