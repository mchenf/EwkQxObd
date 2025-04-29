using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public class EqoIqxProduct
    {
        public required string SerialNumber { get; set; }
        public required EqoContract Contract { get; set; }
        public required EqoAccount ShipTo { get; set; }

        public uint AccountNumber { get => ShipTo.AccountID; }
        public string PartnerName { get => ShipTo.PartnerName; }

        public long ContractNumber { get => Contract.Id; }

    }
}
