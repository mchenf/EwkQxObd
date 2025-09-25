using EwkQxObd.Core.Abstraction;
using EwkQxObd.Core.Model.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public partial class EqoContractObject
    {

        public long Id { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public required string SerialNumber { get; set; }

        public required string InstrumentType { get; set; }

        [Column("Contract")]
        public long ContractId { get; set; }
        [Column("ShipTo")]
        public int ShipToId { get; set; }

        public EqoContract? Contract { get; set; }
        public EqoAccount? ShipTo { get; set; }

        public long AccountNumber { get => ShipTo == default ? -1 : ShipTo.PartnerId; }
        public string PartnerName { get => ShipTo == default ? string.Empty : ShipTo.PartnerName; }

        public long ContractNumber { get => Contract == default ? -1 : Contract.ContractNumber; }

        [NotMapped]
        public Syngio? InstrumentConnected { get; set; }


    }
}
