using Microsoft.AspNetCore.Http;
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
    }
}
