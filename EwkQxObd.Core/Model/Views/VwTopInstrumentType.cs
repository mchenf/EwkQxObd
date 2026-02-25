using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public sealed class VwTopInstrumentType : IqxInstrumentTypeBase
    {
        [Column(nameof(Usage), TypeName = "int")]
        public int Usage { get; set; }
    }
}
