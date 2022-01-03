using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrainningCSharp.Model
{
    public enum Paymentterm
    {
        [Description("0 Days")]
        Zero_Day,
        [Description("7 Days")]
        Seven_Day,
        [Description("30 Days")]
        Thirty_Day
    }
}
