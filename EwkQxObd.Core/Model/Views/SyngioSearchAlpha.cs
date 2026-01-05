using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public class SyngioSearchAlpha
    {
        public string System { get; set; } = string.Empty;
        public char Initial { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
