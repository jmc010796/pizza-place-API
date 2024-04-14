using Microsoft.AspNetCore.Mvc;
using PizzaPlaceAPI.Controllers.Model;
using PizzaPlaceAPI.DB;

namespace PizzaPlaceAPI.Controllers
{
    // Menu Controller
    // Controller for Getting Menu Items
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IPizzaPlaceRepository repository;

        public MenuController(IPizzaPlaceRepository repository)
        {
            this.repository = repository;
        }

        // GET api/Menu/GetAllPizza/{page}?pageSize={pageSize}
        // Get Paginated Unfiltered Pizza list
        [HttpGet]
        [Route("{page}")]
        public IActionResult GetAllPizza(int page = 1, int pageSize = 25)
        {
            MenuResponse resp = new MenuResponse();
            resp.itemCount = this.repository.GetPizzaCount();
            resp.page = page;
            resp.pageSize = pageSize;
            resp.pizzaList = this.PaginateQuery(
                this.repository.GetPizzaList(),
                page,
                pageSize
            ).ToList();
            return Ok(resp);
        }

        // GET api/Menu/GetCategory/{page}?categId={categId}&pageSize={pageSize}
        // Get Paginated Pizza list by Category
        [HttpGet]
        [Route("{page}")]
        public IActionResult GetCategory(int categId, int page = 1, int pageSize = 25)
        {
            MenuResponse resp = new MenuResponse();
            resp.itemCount = this.repository.GetPizzaCountByCategory(categId);
            resp.page = page;
            resp.pageSize = pageSize;
            resp.category = this.repository.GetCategoryName(categId);
            if (resp.category != null)
                resp.pizzaList = this.PaginateQuery(
                    this.repository.GetPizzaListByCategory(categId),
                    page,
                    pageSize
                ).ToList();
            return Ok(resp);
        }

        // POST api/Menu/SearchPizza/{page}?pageSize={}
        // Get Paginated list of Pizzas Matching Search condition
        [HttpPost]
        [Route("{page}")]
        public IActionResult SearchPizza([FromBody] SearchQuery query, int page = 1, int pageSize = 25)
        {
            MenuResponse resp = new MenuResponse();
            IQueryable<MenuItem> items = this.repository.SearchPizza(query);
            resp.itemCount = items.Count();
            resp.page = page;
            resp.pageSize = pageSize;
            resp.query = query;
            resp.pizzaList = this.PaginateQuery(items, page, pageSize).ToList();
            return Ok(resp);
        }

        private IQueryable<MenuItem> PaginateQuery(IQueryable<MenuItem> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
