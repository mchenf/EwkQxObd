using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public long Id { get; set; }

        [Required]
        [Column(nameof(TicketNumber), TypeName = "nvarchar(16)")]
        public string TicketNumber { get; set; } = string.Empty;

        [Required]
        [Column(nameof(Description), TypeName = "nvarchar(255)")]
        public string Description { get; set; } = string.Empty;

        [Column(nameof(Requester))]
        public long? RequesterId { get; set; }
        public EqoContactInfo? Requester { get; set; }

        [Column(nameof(Operations))]
        public long? OperationsId { get; set; }
        public EqoContactInfo? Operations { get; set; }

    }
}
