using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("InstrumentType", Schema = "iqx")]
    public class IqxInstrumentType : IqxInstrumentTypeBase
    {
        [Column(nameof(Description), TypeName = "ntext")]
        public string Description { get; set; } = string.Empty;
    }
}
