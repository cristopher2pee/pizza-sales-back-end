using PizzaSalesChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;

namespace PizzaSalesChallenge.Infrastructure.DataAccess.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository _OrderRepository { get; }
        IOrderDetailRepository _OrderDetailsRepository { get; }
        IPizzaRepository _PizzaRepository { get; }
        IPizzaTypeRepository _PizzaTypeRepository { get; }

        Task<int> SaveChangeAsync();
    }
}
