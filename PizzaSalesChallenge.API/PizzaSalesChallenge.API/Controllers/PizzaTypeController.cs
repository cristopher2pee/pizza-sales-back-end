using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Business.DTO.Response;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Business.Utilities;

namespace PizzaSalesChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaTypeController : ControllerBase
    {
        private readonly IPizzaTypeService _pizzaTypeService;

        public PizzaTypeController(IPizzaTypeService pizzaTypeService)
        {
            _pizzaTypeService = pizzaTypeService;
        }

        [HttpPost]
        [ProducesResponseType<PizzaTypeResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPizzaType([FromBody] PizzaTypeRequest req)
        {
            var result = await _pizzaTypeService.CreatePizzaType(req.ConverToEntity(true));
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpPut]
        [ProducesResponseType<PizzaTypeResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePizzaType([FromBody] PizzaTypeRequest req)
        {
            var result = await _pizzaTypeService.UpdatePizzaType(req.ConverToEntity(false));
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpGet]
        [ProducesResponseType<PageResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] BaseFilter filter)
        {
            var result = await _pizzaTypeService.GetAll(filter);

            return Ok(new PageResponse(result.data.ConvertToResponseList(), 
                result.totalRow, result.TotalRowPage));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<PizzaTypeResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _pizzaTypeService.GetPizzaTypeById(id);
            if (result is null) return NotFound();

            return Ok(result.ConvertToResponse());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var result = await _pizzaTypeService.DeletePizzaType(id);
            return result ? Ok() : BadRequest();
        }

    }
}
