using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Filter
{
    public class OrderFilter : BaseFilter
    {
        public int OrderNo { get; set; } = 0;
    }
}
