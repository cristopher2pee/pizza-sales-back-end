using Microsoft.AspNetCore.Http;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Services.Interface
{
    public interface IOrderDetailsService
    {
        Task ImportCSVFile(IFormFile file);

        Task<OrderDetails?> CreateOrderDetails(OrderDetails param);
        Task<OrderDetails?> UpdateOrderDetails(OrderDetails param);
        Task<bool> DeleteOrderDetails(Guid id);
        Task<OrderDetails?> GetOrderDetailsById(Guid id);
        Task<(IEnumerable<OrderDetails> data, int totalRow, int TotalRowPage)> GetAll(BaseFilter filter);

    }
}
