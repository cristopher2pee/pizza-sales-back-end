using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Business.DTO.Response;
using PizzaSalesChallenge.Business.Services;
using PizzaSalesChallenge.Business.Services.Interface;

namespace PizzaSalesChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailService;
        public OrderDetailController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailService = orderDetailsService;
        }

        [HttpGet]
        [ProducesResponseType<PageResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery]BaseFilter filter)
        {
            var result = await _orderDetailService.GetAll(filter);

            return Ok(new PageResponse(result.data.ConvertToResponseList(),
                 result.totalRow, result.TotalRowPage));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<OrderDetailsResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _orderDetailService.GetOrderDetailsById(id);
            if (result is null) return NotFound();

            return Ok(result.ConvertToResponse());
        }

        [HttpPost]
        [ProducesResponseType<OrderDetailsResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrderDetails([FromBody] OrderDetailsRequest req)
        {
            var result = await _orderDetailService.CreateOrderDetails(req.ConvertToEntity(true));
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpPut]
        [ProducesResponseType<OrderDetailsResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderDetails([FromBody] OrderDetailsRequest req)
        {
            var result = await _orderDetailService.UpdateOrderDetails(req.ConvertToEntity());
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var result = await _orderDetailService.DeleteOrderDetails(id);
            return result ? Ok() : BadRequest();
        }

    }
}
