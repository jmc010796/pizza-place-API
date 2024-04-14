namespace PizzaPlaceAPI.Controllers.Model
{
    public class MenuResponse
    {
        public int itemCount { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<MenuItem> pizzaList { get; set; }
        public string? category { get; set; }
        public SearchQuery query { get; set; }
    }
}
