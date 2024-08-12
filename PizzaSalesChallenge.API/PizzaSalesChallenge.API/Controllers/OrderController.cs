using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Business.DTO.Response;
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
        private readonly IOrderDetailsService _orderDetailsService;
        public OrderController(IOrderService orderService, IOrderDetailsService orderDetailsService)
        {
            _orderService = orderService;
            _orderDetailsService = orderDetailsService;
        }


        [HttpGet]
        [ProducesResponseType<PageResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] OrderFilter filter)
        {
            var result = await _orderService.GetAll(filter);

            return Ok(new PageResponse(result.data.ConvertToResponseList(),
                result.totalRow, result.TotalRowPage));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _orderService.GetOrderById(id);
            if (result is null) return NotFound();

            return Ok(result.ConvertToResponse());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (!result) return NotFound();

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody] OrderRequest req)
        {

            var isvalid = await _orderService.isOrderCodeTaken(req.OrderNo);
            if(isvalid) return BadRequest("Order no is already taken.");

            var order = req.ConvertToEntity(true);

            var result = await _orderService.CreateOrder(req);
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpPut]
        [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderRequest req)
        {
            var order = req.ConvertToEntity(false);
            var result = await _orderService.UpdateOrder(order);

            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }



    }
}
