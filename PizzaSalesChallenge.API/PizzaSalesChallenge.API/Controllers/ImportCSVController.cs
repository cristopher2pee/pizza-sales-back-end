using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.DTO.Response;
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
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportOrderCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest();

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest();

            await _orderService.ImportCSVFile(file);

            return Ok(new ProblemDetails
            {
                Title = "Succesfully Import CSV File",
                Status = StatusCodes.Status200OK,
            });
        }

        [HttpPost("order-detail/import-csv")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportOrderDetailCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest();


            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest();

            await _orderDetailService.ImportCSVFile(file);

            return Ok(new ProblemDetails
            {
                Title = "Succesfully Import CSV File",
                Status = StatusCodes.Status200OK,
            });
        }

        [HttpPost("pizza/import-csv")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportPizzaCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest();

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest();

            await _pizzaService.ImportCSVFile(file);

            return Ok(new ProblemDetails
            {
                Title = "Succesfully Import CSV File",
                Status = StatusCodes.Status200OK,
            });
        }

        [HttpPost("pizza-type/import-csv")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportPizzaTypeCSV(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File not provided or Empty.");

            if (!FileChecker.IsFileCSV(file.FileName))
                return BadRequest("CSV files are the only accepted format.");

            await _pizzaTypeService.ImportCSVFile(file);

            return Ok(new ProblemDetails
            {
                Title = "Succesfully Import CSV File",
                Status = StatusCodes.Status200OK,
            });
        }
    }
}
