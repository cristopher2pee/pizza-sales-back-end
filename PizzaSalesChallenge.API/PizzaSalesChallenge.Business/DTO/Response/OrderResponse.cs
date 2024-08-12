using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Response
{
    public class OrderResponse : BaseDto
    {
        public int OrderNo { get; set; }
        public DateTime DateTimeOrder { get; set; }

        public List<OrderDetailsResponse> OrderDetails { get; set; } = [];
    }

    public class OrderDetailsResponse : BaseDto
    {
        public Guid PizzaId { get; set; }
        public PizzaResponse? Pizza { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public OrderResponse? OrderResponse { get; set; }

    }
}
