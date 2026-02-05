using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    [Flags]
    public enum VinlkStatus
    {


        OK  =               0b_0000_0000_0000_0000,
        MissContract =      0b_0000_0000_0000_0001,
        MissSystem =        0b_0000_0000_0000_0010,
        MissNetwork =       0b_0000_0000_0000_0100,
        LinkedToZero =      0b_0000_0000_0000_1000,

        LinkedToNull =      0b_0000_0000_0001_0000,
        MissAccount =       0b_0000_0000_0010_0000
    }
}
