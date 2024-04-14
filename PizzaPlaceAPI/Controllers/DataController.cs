using Microsoft.AspNetCore.Mvc;
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
        // Truncate Tables and Import new Dataset from CSV
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
