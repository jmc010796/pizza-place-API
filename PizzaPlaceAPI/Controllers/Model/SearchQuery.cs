namespace PizzaPlaceAPI.Controllers.Model
{
    public class SearchQuery
    {
        public string? name { get; set; }
        public string? size { get; set; }
        public int? categId { get; set; }
        public List<string>? contains { get; set; }
        public List<string>? exclude { get; set; }
    }
}
