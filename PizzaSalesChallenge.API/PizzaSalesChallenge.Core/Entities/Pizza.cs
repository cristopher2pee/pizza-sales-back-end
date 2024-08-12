using PizzaSalesChallenge.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Entities
{
    public class Pizza : BaseEntity
    {
        public string PizzaCode { get; set; } 
        public Guid PizzaTypeId { get; set; }
        public virtual PizzaType PizzaType { get; set; }
        public PizzaSize Size { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
