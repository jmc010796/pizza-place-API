using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(recipe_id))]
    public class Recipe
    {
        public string recipe_id { get; set; }
        public string name { get; set; }
        public int category_id { get; set; }
    }
}
