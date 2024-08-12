using PizzaSalesChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Infrastructure.DataAccess
{
    public interface IRepository
    {

    }

    public interface IOrderRepository : IGenericRepository<Order> { };
    public interface IOrderDetailRepository : IGenericRepository<OrderDetails> { };
    public interface IPizzaRepository : IGenericRepository<Pizza> { };
    public interface IPizzaTypeRepository : IGenericRepository<PizzaType> { };
}
