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
        public int Id { get; set; }

        public int TicketId { get; set; }
        public required EqoTicketSource Ticket { get; set; }

        public int ContractId { get; set; }
        public required EqoContract Contract { get; set; }

    }
}
