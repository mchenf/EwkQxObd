using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public class EqoAccount
    {
        public uint AccountID { get; set; }
        public string PartnerName { get; set; } = string.Empty;
        public Guid GeisID { get; set; }
    }
}
