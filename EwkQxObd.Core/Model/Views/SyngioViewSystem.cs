using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public class SyngioViewSystem
    {
        public string System { get; set; } = string.Empty;
        public int Networks { get; set; } = 0;
        [Column("Network Linked")]
        public int Network_Linked { get; set; } = 0;
        public int Instruments { get; set; } = 0;
        [Column("Instrument Linked")]
        public int Instrument_Linked { get; set; } = 0;
    }
}
