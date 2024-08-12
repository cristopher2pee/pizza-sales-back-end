using Microsoft.AspNetCore.Http;
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
        Task<bool> ImportCSVFile(IFormFile file);
        Task<Pizza?> GetPizzaByCode(string code);
        
    }
}
