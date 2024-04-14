using PizzaPlaceAPI.Controllers.Model;

namespace PizzaPlaceAPI.DB
{
    public interface IPizzaPlaceRepository
    {
        public void BulkInsert(string table, string file);
        public IQueryable<MenuItem> GetPizzaList();
        public IQueryable<MenuItem> GetPizzaListByCategory(int category);
        public IQueryable<MenuItem> SearchPizza(SearchQuery query);
        public int GetPizzaCount();
        public int GetPizzaCountByCategory(int categId);
        public string? GetCategoryName(int categId);
    }
}
