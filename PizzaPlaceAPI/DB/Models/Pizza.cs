using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(pizza_id))]
    public class Pizza
    {
        public string pizza_id { get; set; }
        public string recipe_id { get; set; }
        public string size { get; set; }
        public double price { get; set; }
    }
}
