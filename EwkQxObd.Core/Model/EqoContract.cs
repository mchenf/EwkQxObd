using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent a Contract object
    /// </summary>
    public class EqoContract
    {
        public long Id { get; set; }
        public long ContractNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
