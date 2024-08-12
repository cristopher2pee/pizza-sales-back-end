using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Model
{
    public class PizzaCSV
    {
        public string pizza_id { get; set; }
        public string pizza_type_id { get; set; }
        public string size { get; set; }
        public decimal price { get; set; }
    }
}
