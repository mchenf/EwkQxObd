using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent a ticket source, where this ticket is from
    /// </summary>
    [Table("TicketSource", Schema = "eqo")]
    public class EqoTicketSource
    {
        public long id { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string TicketDescription { get; set; } = string.Empty;
        
        public IEnumerable<EqoContract>? IqxContracts { get; set; }


    }
}
