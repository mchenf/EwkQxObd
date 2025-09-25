using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Abstraction
{
    public enum FlatTextLod
    {
        None = 0,

        Customer = 0b_0000_0000_0000_0001,
        Org_Only = 0b_0000_0000_0000_0010,
        Internal_Only = 0b_0000_0000_0000_0100,

        Organisation = Customer | Org_Only,
        Internal = Organisation | Internal_Only,

        Full = 0b_1111_1111_1111_1111
    }
}
