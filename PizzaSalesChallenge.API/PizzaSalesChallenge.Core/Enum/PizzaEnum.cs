using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Core.Enum
{
    internal class PizzaEnum
    {
    }

    public enum PizzaCategoryType
    {
        [Description("None")]
        None = 0,
        [Description("Chicken")]
        Chicken = 1,
        [Description("Classic")]
        Classic = 2,
        [Description("Suppreme")]
        Suppreme = 3,
        [Description("Veggie")]
        Veggie = 4,
    }

    public enum PizzaSize
    {
        [Description("None")]
        None = 0,
        [Description("Snmall")]
        Small = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Large")]
        Large = 3
    }
}
