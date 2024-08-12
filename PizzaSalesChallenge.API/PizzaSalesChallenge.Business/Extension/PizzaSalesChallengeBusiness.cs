using Microsoft.Extensions.DependencyInjection;
using PizzaSalesChallenge.Business.Services;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Infrastructure.DataAccess;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Extension
{
    public static class PizzaSalesChallengeBusiness
    {
        public static IServiceCollection PizzaSalesChallengeBusinessExtension(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IOrderDetailRepository, OrderDetailsRepository>();
            service.AddScoped<IPizzaRepository, PizzaRepository>();
            service.AddScoped<IPizzaTypeRepository, PizzaTypeRepository>();

            service.AddScoped<IPizzaService, PizzaServices>();
            service.AddScoped<IPizzaTypeService, PizzaTypeService>();
            service.AddScoped<IOrderService, OrderService>();

            return service;
        }
    }
}
