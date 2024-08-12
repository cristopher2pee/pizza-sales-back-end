using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Business.DTO.Response;
using PizzaSalesChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public static class PizzaMapper
    {
        public static PizzaResponse ConvertToResponse(this Pizza e)
        {
            return new PizzaResponse
            {
                Id = e.Id,
                PizzaCode = e.PizzaCode,
                PizzaTypeId = e.PizzaTypeId,
                PizzaType = e.PizzaType != null ? e.PizzaType.ConvertToResponse() : null,
                Size = e.Size,
                Price = e.Price
            };
        }

        public static IEnumerable<PizzaResponse> ConvertToResponseList(this IEnumerable<Pizza> e)
            => e.Select(f => f.ConvertToResponse());

        public static Pizza ConvertToEntity(this PizzaRequest e, bool isNew = false)
        {
            return new Pizza
            {
                Id = isNew ? Guid.NewGuid() : e.Id,
                PizzaCode = e.PizzaCode,
                PizzaTypeId = e.PizzaTypeId,
                Size = e.Size,
                Price = e.Price,
            };
        }
    }
}
