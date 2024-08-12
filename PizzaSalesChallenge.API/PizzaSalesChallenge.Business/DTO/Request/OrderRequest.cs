using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Request
{
    public class OrderRequest : BaseDto
    {
        public int OrderNo { get; set; }
        public List<OrderDetailsRequest> OrderDetails { get; set; } = [];
        public DateTime DateTimeOrder { get; set; } 

    }

    public class OrderDetailsRequest : BaseDto
    {
        public Guid PizzaId { get; set; }
        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
    }
}
