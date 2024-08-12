using CsvHelper.Configuration;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public sealed class PizzaRecordMap : ClassMap<PizzaCSV>
    {
        public PizzaRecordMap()
        {
            Map(m => m.pizza_type_id).Name("pizza_type_id");
            Map(m => m.pizza_id).Name("pizza_id");
            Map(m => m.size).Name("size");
            Map(m => m.price).Name("price");
        }
    }
}
