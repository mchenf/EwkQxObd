using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{


    [Table("TicketSourceContract", Schema = "rel")]
    public class RelTicketSourceContract
    {
        [Key]
        public long Id { get; set; }

        public long TicketId { get; set; }
        public required EqoTicketSource Ticket { get; set; }

        public long ContractId { get; set; }
        public required EqoContract Contract { get; set; }

    }
}
