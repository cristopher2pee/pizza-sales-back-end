using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Business.Utilities;

namespace PizzaSalesChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportCSVController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailsService _orderDetailService;
        private readonly IPizzaService _pizzaService;
        private readonly IPizzaTypeService _pizzaTypeService;

        public ImportCSVController(
            IOrderService orderService,
            IOrderDetailsService orderDetailsService,
            IPizzaService pizzaService,
            IPizzaTypeService pizzaTypeService
            )
        {
            _orderDetailService = orderDetailsService;
            _orderService = orderService;
            _pizzaService = pizzaService;
            _pizzaTypeService = pizzaTypeService;
        }

        [HttpPost("order/import-csv")]
        public async Task<IActionResult> ImportOrderCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            await _orderService.ImportCSVFile(file);
            return Ok();
        }

        [HttpPost("order-detail/import-csv")]
        public async Task<IActionResult> ImportOrderDetailCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            await _orderDetailService.ImportCSVFile(file);
            return Ok();
        }

        [HttpPost("pizza/import-csv")]
        public async Task<IActionResult> ImportPizzaCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            await _pizzaService.ImportCSVFile(file);
            return Ok();
        }

        [HttpPost("pizza-type/import-csv")]
        public async Task<IActionResult> ImportPizzaTypeCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            await _pizzaTypeService.ImportCSVFile(file);
            return Ok();
        }
    }
}
