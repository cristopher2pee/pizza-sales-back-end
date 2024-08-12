using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Filter
{

    public class BaseFilter
    {
        public string? search { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;

    }

}
