using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaPlaceAPI.Controllers.Model;
using PizzaPlaceAPI.DB;

namespace PizzaPlaceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IPizzaPlaceRepository repository;

        public SalesController(IPizzaPlaceRepository repository)
        {
            this.repository = repository;
        }

        // GET api/Sales/GetAllSalesDetails/{page}?pageSize={pageSize}
        // Get Paginated Unfiltered Pizza list
        [HttpGet]
        [Route("{page}")]
        public IActionResult GetAllSalesDetails(int page = 1, int pageSize = 25)
        {
            SalesResponse resp = new SalesResponse();
            resp.itemCount = this.repository.GetSalesCount();
            resp.page = page;
            resp.pageSize = pageSize;
            resp.salesList = this.PaginateQuery(
                this.repository.GetAllSales(),
                page,
                pageSize
            ).ToList();
            return Ok(resp);
        }
        private IQueryable<SalesItem> PaginateQuery(IQueryable<SalesItem> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
