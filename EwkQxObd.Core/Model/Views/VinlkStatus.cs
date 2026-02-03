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
        MissContract =      0b_0000_0000_0000_0001,
        MissSystem =        0b_0000_0000_0000_0010,
        MissNetwork =       0b_0000_0000_0000_0100,
        GeisNotLinked =     0b_0000_0000_0000_1000,
        MissGeis =          0b_0000_0000_0001_0000,
        MissAccount =       0b_0000_0000_0010_0000
    }
}
