using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Installation
{
    [Flags]
    enum EqoInstallStatus
    {
        None            = 0b_0000_0000_0000_0000,
        DbFileExist     = 0b_0000_0000_0000_0001,


        Installed       = DbFileExist
    }
}
