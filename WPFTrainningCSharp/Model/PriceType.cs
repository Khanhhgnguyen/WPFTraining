using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrainningCSharp.Model
{
    public enum PriceType
    {
        [Description("Regular-Price")]
        Regular_Price = 0,
        [Description("Wholesale-Price")]
        Wholesale_Price = 1,
        [Description("Internal-Price")]
        Internal_Price = 2
    }
}
