using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("EwkxInstrumentType", Schema = "dbo")]
    public class EwkxInstrumentType
    {
        [Key]
        [Column(nameof(InstrumentTypeID), TypeName = "int")]
        public int InstrumentTypeID { get; set; }


        [Column(nameof(Name), TypeName = "nvarchar(50)")]
        public string Name { get; set; } = string.Empty;

        [Column(nameof(ShortName), TypeName = "nvarchar(20)")]
        public string ShortName { get; set; } = string.Empty;

        [Column(nameof(Description), TypeName = "ntext")]
        public string Description { get; set; } = string.Empty;
    }
}
