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
    public static class OrderDetailMapper
    {
        public static OrderDetailsResponse ConvertToResponse(this OrderDetails e)
        {
            return new OrderDetailsResponse
            {
                Id = e.Id,
                OrderId = e.OrderId,
                OrderResponse = e.Order != null ? e.Order.ConvertToResponse() : null,
                PizzaId = e.Id,
                Pizza = e.Pizza != null ? e.Pizza.ConvertToResponse() : null
            };
        }

        public static IEnumerable<OrderDetailsResponse> ConvertToResponseList(this IEnumerable<OrderDetails> e)
            => e.Select(e => e.ConvertToResponse());

        public static OrderDetails ConvertToEntity(this OrderDetailsRequest e, bool isNew = false)
        {
            return new OrderDetails
            {
                Id = isNew ? Guid.NewGuid() : e.Id,
                OrderId = e.OrderId,
                PizzaId = e.PizzaId,
                Quantity = e.Quantity,
            };
        }


    }
}
