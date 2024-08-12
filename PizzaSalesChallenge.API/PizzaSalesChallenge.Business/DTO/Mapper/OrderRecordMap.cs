using CsvHelper.Configuration;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public sealed class OrderRecordMap : ClassMap<OrderCSV>
    {
        public OrderRecordMap()
        {
            Map(m => m.order_id).Name("order_id");
            Map(m => m.date).Name("date");
            Map(m => m.time).Name("time");
        }
    }
}
