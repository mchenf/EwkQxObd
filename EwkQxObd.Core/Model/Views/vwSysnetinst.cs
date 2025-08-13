using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public class vwSysnetinst
    {
        [Column("InstrumentId")]
        public long id { get; set; }

        [Column("System", TypeName = "char(16)")]
        public string System { get; set; } = string.Empty;

        [Column("QueryTimeStamp", TypeName = "datetime2")]
        public DateTime QueryTimeStamp { get; set; }

        [Column("NetworkName", TypeName = "nvarchar(64)")]
        public string NetworkName { get; set; } = string.Empty;

        [Column("NetworkId", TypeName = "bigint")]
        public long NetworkId { get; set; }

        [Column("LinkedAccount", TypeName = "uniqueidentifier")]
        public Guid LinkedAccountGuid { get; set; }

        [Column("InstrumentGroup", TypeName = "nvarchar(64)")]
        public string InstrumentGroup { get; set; } = string.Empty;

        [Column("SerialNumber", TypeName = "nvarchar(50)")]
        public string SerialNumber { get; set; } = string.Empty;

        [Column("InstrumentName", TypeName = "nvarchar(64)")]
        public string InstrumentName { get; set; } = string.Empty;
    }
}
