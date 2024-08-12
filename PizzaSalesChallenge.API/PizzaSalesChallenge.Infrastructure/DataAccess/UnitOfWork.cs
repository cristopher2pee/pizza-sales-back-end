using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using PizzaSalesChallenge.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PizzaDatabaseContext _context;
        public IOrderRepository _OrderRepository { get; set; }
        public IOrderDetailRepository _OrderDetailsRepository { get; set; }
        public IPizzaRepository _PizzaRepository { get; set; }
        public IPizzaTypeRepository _PizzaTypeRepository { get; set; }
        public UnitOfWork(
            PizzaDatabaseContext context,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IPizzaRepository pizzaRepository,
            IPizzaTypeRepository pizzaTypeRepository
            )
        {
            _context = context;
            _OrderRepository = orderRepository;
            _OrderDetailsRepository = orderDetailRepository;
            _PizzaRepository = pizzaRepository;
            _PizzaTypeRepository = pizzaTypeRepository;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
