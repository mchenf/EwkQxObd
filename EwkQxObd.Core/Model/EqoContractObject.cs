using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent an IQX product, this object links instrument to contract and to account
    /// </summary>
    /// 
    [Table("ContractObject", Schema = "eqo")]
    public class EqoContractObject
    {

        public long id { get; set; }
        public required string SerialNumber { get; set; }

        public required string InstrumentType { get; set; }

        [Column("Contract")]
        public long EqoContractId { get; set; }
        [Column("ShipTo")]
        public int EqoAccountId { get; set; }

        public EqoContract? Contract { get; set; }
        public EqoAccount? ShipTo { get; set; }

        public long AccountNumber { get => ShipTo == default ? -1 : ShipTo.PartnerId; }
        public string PartnerName { get => ShipTo == default ? string.Empty : ShipTo.PartnerName; }

        public long ContractNumber { get => Contract == default ? -1 : Contract.ContractNumber; }

    }
}
