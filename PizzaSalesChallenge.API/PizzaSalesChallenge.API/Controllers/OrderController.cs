using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.Services;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Business.Utilities;

namespace PizzaSalesChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("import-csv")]
        public async Task<IActionResult> ImportCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            await _orderService.ImportCSVFile(file);
            return Ok();
        }
    }
}
