using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public class IqxProduct
    {
        public long SerialNumber { get; set; }
        public IqxNetwork? Network { get; set; }
        public EqoAccount ShipTo { get; set; }
        public EqoContract Contract { get; set; }

        public IqxProduct(EqoAccount shipTo, EqoContract contract)
        {
            ShipTo = shipTo;
            Contract = contract;
        }
    }
}
