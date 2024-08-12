using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Entities
{
    public class OrderDetails : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }
        public int Quantity { get; set; }
    }
}
