using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(order_id))]
    public class OrderStamp
    {
        public int order_id { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
    }
}
