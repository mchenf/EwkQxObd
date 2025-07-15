using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent an Account
    /// </summary>
    public class EqoAccount
    {
        public int Id { get; set; }
        public long PartnerId { get; set; } = long.MinValue;
        public string PartnerName { get; set; } = string.Empty;
        public Guid GeisID { get; set; }

        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

    }
}
