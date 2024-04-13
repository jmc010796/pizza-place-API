namespace PizzaPlaceAPI.DB.Models
{
    public class OrderDetail
    {
        public uint order_details_id { get; set; }
        public uint order_id { get; set; }
        public string pizza_id { get; set; }
        public uint quantity { get; set; }
    }
}
