using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Model
{
    public class OrderCSV
    {
        public int order_id { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
    }
}
