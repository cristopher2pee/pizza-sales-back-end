using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Infrastructure.DataAccess
{

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(PizzaDatabaseContext context) : base(context)
        {
        }
    }

    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailRepository
    {
        public OrderDetailsRepository(PizzaDatabaseContext context) : base(context)
        {
        }
    }

    public class PizzaRepository : GenericRepository<Pizza>, IPizzaRepository
    {
        public PizzaRepository(PizzaDatabaseContext context) : base(context)
        {
        }
    }

    public class PizzaTypeRepository : GenericRepository<PizzaType>, IPizzaTypeRepository
    {
        public PizzaTypeRepository(PizzaDatabaseContext context) : base(context)
        {
        }
    }
}
