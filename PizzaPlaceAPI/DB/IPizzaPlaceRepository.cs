using PizzaPlaceAPI.Controllers.Model;
using PizzaPlaceAPI.DB.Models;

namespace PizzaPlaceAPI.DB
{
    public interface IPizzaPlaceRepository
    {
        public void BulkInsert(string datasetType, string file);
        public OrderStamp InsertOrder();
        public List<OrderItem> InsertOrderDetails(int orderId, List<OrderItem> orderItem);
        public IQueryable<MenuItem> GetPizzaList();
        public IQueryable<MenuItem> GetPizzaListByCategory(int category);
        public IQueryable<MenuItem> SearchPizza(SearchQuery query);
        public int GetPizzaCount();
        public int GetPizzaCountByCategory(int categId);
        public string? GetCategoryName(int categId);
        public IQueryable<SalesItem> GetAllSales();
        public int GetSalesCount();
    }
}
