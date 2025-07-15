using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent an IQX product, this object links instrument to contract and to account
    /// </summary>
    public class EqoIqxProduct
    {
        public required string SerialNumber { get; set; }
        public required EqoContract Contract { get; set; }
        public required EqoAccount ShipTo { get; set; }

        public long AccountNumber { get => ShipTo.PartnerId; }
        public string PartnerName { get => ShipTo.PartnerName; }

        public long ContractNumber { get => Contract.Id; }

    }
}
