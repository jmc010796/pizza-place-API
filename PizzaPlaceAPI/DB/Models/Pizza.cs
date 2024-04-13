namespace PizzaPlaceAPI.DB.Models
{
    public class Pizza
    {
        public string pizza_id { get; set; }
        public string pizza_type_id { get; set; }
        public string size { get; set; }
        public float price { get; set; }
    }
}
