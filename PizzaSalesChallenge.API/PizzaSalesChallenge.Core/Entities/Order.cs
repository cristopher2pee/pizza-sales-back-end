using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Entities
{
    public class Order : BaseEntity
    {
        public int OrderNo { get; set; }
        public DateTime DateTimeOrder { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
