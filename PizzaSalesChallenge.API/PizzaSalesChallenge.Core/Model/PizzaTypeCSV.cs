using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Model
{
    public class PizzaTypeCSV
    {
        public string pizza_type_id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public string ingredients { get; set; } = string.Empty;
    }
}
