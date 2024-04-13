using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaPlaceAPI.DB;
using PizzaPlaceAPI.Controllers.Model;

namespace PizzaPlaceAPI.Controllers
{
    // Data Controller
    // Controller for Handling Data sets
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IPizzaPlaceRepository repository;

        public DataController(IPizzaPlaceRepository repository)
        {
            this.repository = repository;
        }

        // POST api/Data/UploadDataSet
        // type = Name of Pizza Place Dataset (orders, order_details, pizzas, pizza_types)
        // file = CSV file to import
        [HttpPost]
        public async Task<IActionResult> UploadDataSet([FromForm] string type, [FromForm] IFormFile file)
        {
            DataResponse resp = new DataResponse();
            using (StreamReader sr = new StreamReader(file.OpenReadStream()))
            {
                List<string> headers = new List<string>();
                string content = await sr.ReadToEndAsync();
                this.repository.BulkInsert(type, content.Replace("\r", ""));
                resp.status = 200;
                resp.message = "File Upload Ended OK";
                return Ok(resp);
            }
        }
    }
}
