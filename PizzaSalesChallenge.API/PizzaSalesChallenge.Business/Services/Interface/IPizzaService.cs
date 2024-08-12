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
    public interface IPizzaService
    {
        Task ImportCSVFile(IFormFile file);
        Task<Pizza?> GetPizzaByCode(string code);
        Task<Pizza?> UpdatePizza(Pizza pizzaType);
        Task<Pizza?> CreatePizza(Pizza param);
        Task<bool> DeletePizza(Guid id);
        Task<Pizza?> GetPizzaById(Guid id);
        Task<(IEnumerable<Pizza> data, int totalRow, int TotalRowPage)> GetAll(BaseFilter filter);

    }
}
