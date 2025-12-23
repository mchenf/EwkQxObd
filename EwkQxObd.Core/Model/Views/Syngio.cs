using EwkQxObd.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public partial class Syngio
    {
        [Column("InstrumentId", TypeName = "int")]
        public int Id { get; set; }

        [Column("System", TypeName = "char(16)")]
        public string System { get; set; } = string.Empty;

        [Column("QueryTimeStamp", TypeName = "datetime2")]
        public DateTime QueryTimeStamp { get; set; }

        [Column("NetworkName", TypeName = "nvarchar(64)")]
        public string? NetworkName { get; set; } = string.Empty;

        [Column("NetworkId", TypeName = "int")]
        public int? NetworkId { get; set; }

        [Column("InstrumentGroup", TypeName = "nvarchar(64)")]
        public string? InstrumentGroup { get; set; } = string.Empty;

        [Column("SerialNumber", TypeName = "nvarchar(50)")]
        public string SerialNumber { get; set; } = string.Empty;

        [Column("LinkedAccount", TypeName = "uniqueidentifier")]
        public Guid LinkedAccountGuid { get; set; }

        [Column("InstrumentName", TypeName = "nvarchar(64)")]
        public string InstrumentName { get; set; } = string.Empty;

        [Column(nameof(AccountName), TypeName = "nvarchar")]
        public string? AccountName { get; set; } = string.Empty;

        [Column("AccountNumber", TypeName = "int")]
        public int? AccountNumber { get; set; }

        [Column("City", TypeName = "nvarchar")]
        public string? City { get; set; } = string.Empty;
        [Column("Street", TypeName = "nvarchar")]
        public string? Street { get; set; } = string.Empty;

        [Column(nameof(Region), TypeName = "nvarchar")]
        public string? Region { get; set; } = string.Empty;

        [Column(nameof(Country), TypeName = "nvarchar")]
        public string? Country { get; set; } = string.Empty;

        [Column(nameof(ContractNumber), TypeName = "int")]
        public int? ContractNumber { get; set; } 
    }
}
