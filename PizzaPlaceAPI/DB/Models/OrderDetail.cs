using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceAPI.DB.Models
{
    [PrimaryKey(nameof(order_detail_id))]
    public class OrderDetail
    {
        public int order_detail_id { get; set; }
        public int order_id { get; set; }
        public string pizza_id { get; set; }
        public int quantity { get; set; }
    }
}
