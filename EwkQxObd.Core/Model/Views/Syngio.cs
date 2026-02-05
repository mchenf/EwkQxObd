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
        [Column("SyngioId", TypeName = "int")]
        public int? Id { get; set; }

        [Column("System", TypeName = "char(16)")]
        public string? System { get; set; } = string.Empty;

        [Column("QueryTimeStamp", TypeName = "datetime2")]
        public DateTime? QueryTimeStamp { get; set; }

        [Column("NetworkName", TypeName = "nvarchar(64)")]
        public string? NetworkName { get; set; } = string.Empty;

        [Column("NetworkId", TypeName = "int")]
        public int? NetworkId { get; set; }

        [Column("InstrumentGroup", TypeName = "nvarchar(64)")]
        public string? InstrumentGroup { get; set; } = string.Empty;

        [Column("SerialNumber", TypeName = "nvarchar(50)")]
        public string? SerialNumber { get; set; } = string.Empty;

        [Column("InstrumentName", TypeName = "nvarchar(64)")]
        public string? InstrumentName { get; set; } = string.Empty;

        public IqxOrganization? ConnectedTo { get; set; }

    }
}
