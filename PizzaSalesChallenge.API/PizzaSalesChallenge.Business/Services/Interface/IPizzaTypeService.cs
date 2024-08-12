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
    public interface IPizzaTypeService
    {
        Task ImportCSVFile(IFormFile file);
        Task<PizzaType?> UpdatePizzaType(PizzaType pizzaType);
        Task<PizzaType?> GetPizzaTypeByCode(string code);

        Task<PizzaType?> CreatePizzaType(PizzaType pizzaType);
        Task<bool> DeletePizzaType(Guid id);
        Task<PizzaType?> GetPizzaTypeById(Guid id);
        Task<(IEnumerable<PizzaType> data, int totalRow, int TotalRowPage)> GetAll(BaseFilter filter);
        
    }
}
