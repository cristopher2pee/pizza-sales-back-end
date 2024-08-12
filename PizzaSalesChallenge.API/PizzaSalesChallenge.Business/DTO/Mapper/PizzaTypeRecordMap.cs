using CsvHelper.Configuration;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public sealed class PizzaTypeRecordMap : ClassMap<PizzaTypeCSV>
    {
        public PizzaTypeRecordMap()
        {
            Map(m => m.pizza_type_id).Name("pizza_type_id");
            Map(m => m.name).Name("name");
            Map(m => m.category).Name("category");
            Map(m => m.ingredients).Name("ingredients");
        }
    }
}
