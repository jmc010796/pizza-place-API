namespace PizzaPlaceAPI.Controllers.Model
{
    public class SalesItem
    {
        public int order_id { get; set; }
        public string pizza_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
    }
}
