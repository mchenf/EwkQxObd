using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public class SyngioViewNetwork
    {
        public string System { get; set; } = string.Empty;
        public long NetworkId { get; set; } = 0;
        public string NetworkName { get; set; } = string.Empty;


        public int Instruments { get; set; } = 0;
        [Column("Instrument Linked")]
        public int Instrument_Linked { get; set; } = 0;

        public string Progress_Instrument
        {
            get => (Instrument_Linked * 100 / Instruments).ToString() + "%";
        }
    }
}
