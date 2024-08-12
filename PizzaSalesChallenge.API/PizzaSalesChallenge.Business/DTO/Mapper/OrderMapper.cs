using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Business.DTO.Response;
using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public static class OrderMapper
    {
        public static Order ConvertRecordToEntity(this OrderCSV record)
        {
            return new Order
            {
                OrderNo = record.order_id,
                DateTimeOrder = Utilities.DateTimeUtilities.ConvertRecordToDateTime(record.date, record.time)
            };
        }

        public static Order ConvertToEntity(this OrderRequest request, bool isNew = false)
        {
            return new Order
            {
                Id = isNew ? Guid.NewGuid() : request.Id,
                OrderNo = request.OrderNo,
                DateTimeOrder = request.DateTimeOrder,
                OrderDetails = request.OrderDetails != null && request.OrderDetails.Any() ?
                    request.OrderDetails.Select(e => new OrderDetails
                    {

                        Id = isNew ? Guid.NewGuid() : e.Id,
                        PizzaId = e.PizzaId,
                        Quantity = e.Quantity,
                        OrderId = e.OrderId

                    }).ToList() : []

            };
        }

        public static OrderResponse ConvertToResponse(this Order e)
        {
            return new OrderResponse
            {
                Id = e.Id,
                OrderNo = e.OrderNo,
                DateTimeOrder = e.DateTimeOrder,
                OrderDetails = e.OrderDetails != null && e.OrderDetails.Any() ?
                    e.OrderDetails.Select(i => new OrderDetailsResponse
                    {
                        Id = i.Id,
                        PizzaId = i.PizzaId,
                        Quantity = i.Quantity,
                        Pizza = i.Pizza != null ? i.Pizza.ConvertToResponse() : null,
                    }).ToList() : []
            };
        }

        public static IEnumerable<OrderResponse> ConvertToResponseList(this IEnumerable<Order> e)
            => e.Select(e=>e.ConvertToResponse());
    }   
}
