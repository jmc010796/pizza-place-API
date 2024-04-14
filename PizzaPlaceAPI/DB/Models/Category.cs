using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(category_id))]
    public class Category
    {
        public int category_id { get; set; }
        public string name { get; set; }
    }
}
