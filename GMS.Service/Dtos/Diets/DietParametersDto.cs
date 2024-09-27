using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Dtos.Diets
{
    public class DietParametersDto : ParametersDto
    {
        public string? MealType { get; set; }
        public string? FoodItem { get; set; }
        public Decimal? Calories { get; set; }
    }
}
