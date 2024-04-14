using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(ingredient_id))]
    public class Ingredient
    {
        public int ingredient_id { get; set; }
        public string name { get; set; }
    }
}
