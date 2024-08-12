using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.Services;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Business.Utilities;

namespace PizzaSalesChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpPost("import-csv")]
        public async Task<IActionResult> ImportCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            var result = await _pizzaService.ImportCSVFile(file);
            return result ? Ok() : BadRequest();
        }
    }

}
