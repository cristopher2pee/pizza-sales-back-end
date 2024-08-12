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
    public class PizzaController : ControllerBase
    {

        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }


        [HttpGet]
        [ProducesResponseType<PageResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAll([FromQuery] BaseFilter filter)
        {
            var result = await _pizzaService.GetAll(filter);

            return Ok(new PageResponse(result.data.ConvertToResponseList(),
                result.totalRow, result.TotalRowPage));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<PizzaResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _pizzaService.GetPizzaById(id);
            if (result is null) return NotFound();

            return Ok(result.ConvertToResponse());
        }


        [HttpPost]
        [ProducesResponseType<PizzaResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPizza([FromBody] PizzaRequest req)
        {
            var result = await _pizzaService.CreatePizza(req.ConvertToEntity(true));
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpPut]
        [ProducesResponseType<PizzaResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePizza([FromBody] PizzaRequest req)
        {
            var result = await _pizzaService.UpdatePizza(req.ConvertToEntity(false));
            if (result is null) return BadRequest();

            return Ok(result.ConvertToResponse());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var result = await _pizzaService.DeletePizza(id);
            return result ? Ok() : BadRequest();
        }

    }

}
