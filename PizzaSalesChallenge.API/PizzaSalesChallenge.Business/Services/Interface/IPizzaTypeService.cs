using Microsoft.AspNetCore.Http;
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
        Task<bool> ImportCSVFile(IFormFile file);
        Task<PizzaType?> UpdatePizzaType(PizzaType pizzaType);
        Task<PizzaType?> GetPizzaTypeByCode(string code);
    }
}
