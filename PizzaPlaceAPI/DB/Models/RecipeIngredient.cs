using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(recipe_ingredient_id))]
    public class RecipeIngredient
    {
        public int recipe_ingredient_id { get; set; }
        public string recipe_id { get; set; }
        public int ingredient_id { get; set; }
    }
}
