using Cooking_School.Core.ModelUsed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking_School.Services.Dtos.AdminDto
{
    public class FavoriteMealDto
    {
        public Meal Meal { get; set; }
        public int Number_of_Favorite { get; set; } 
    }
}
