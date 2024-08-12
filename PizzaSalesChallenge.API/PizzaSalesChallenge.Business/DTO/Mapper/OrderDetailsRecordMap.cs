using CsvHelper.Configuration;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public sealed class OrderDetailsRecordMap : ClassMap<OrderDetailsCSV>
    {
        public OrderDetailsRecordMap()
        {
            Map(m => m.order_details_id).Name("order_details_id");
            Map(m => m.order_id).Name("order_id");
            Map(m => m.pizza_id).Name("pizza_id");
            Map(m => m.quantity).Name("quantity");
        }

    }
}
