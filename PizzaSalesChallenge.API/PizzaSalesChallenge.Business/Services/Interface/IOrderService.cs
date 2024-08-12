using Microsoft.AspNetCore.Http;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Services.Interface
{
    public interface IOrderService
    {
        Task ImportCSVFile(IFormFile file);
        Task<Order?> GetOrderByOrderCode(int code);

        Task<bool> isOrderCodeTaken(int code);
        Task<Order?> CreateOrder(OrderRequest order);
        Task<Order?> UpdateOrder(Order order);
        Task<bool> DeleteOrder(Guid id);
        Task<Order?> GetOrderById(Guid Id);
        Task<(IEnumerable<Order> data, int totalRow, int TotalRowPage)> GetAll(OrderFilter filter);
    }
}
