namespace PizzaPlaceAPI.DB.Models
{
    public class Order
    {
        public uint order_id { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
    }
}
