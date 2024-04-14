namespace PizzaPlaceAPI.Controllers.Model
{
    public class OrderResponse
    {
        public int status { get; set; }
        public string? message { get; set; }
        public List<OrderItem> orders { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
    }
}
