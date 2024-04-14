namespace PizzaPlaceAPI.Controllers.Model
{
    public class SalesResponse
    {
        public int itemCount { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<SalesItem> salesList { get; set; }
    }
}
