using PizzaSalesChallenge.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Request
{
    public class PizzaRequest : BaseDto
    {
        public string PizzaCode { get; set; }
        public Guid PizzaTypeId { get; set; }
        public PizzaSize Size { get; set; }
        public decimal Price { get; set; }
    }
}
