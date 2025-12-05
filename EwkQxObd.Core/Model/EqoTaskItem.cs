using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("TtsTaskItem", Schema = "eqo")]
    public class EqoTaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(nameof(Task), TypeName = "int")]
        public int TaskId { get; set; }
        public EqoTask? Task { get; set; }

        [Required]
        [Column(nameof(Ticket), TypeName = "int")]
        public int TicketId { get; set; }
        public EqoTicketSource? Ticket { get; set; }

        [Required]
        [Column(nameof(CreatedAt), TypeName = "datetime2(7)")]
        public DateTime CreatedAt { get; set; }

        [Column(nameof(Notes), TypeName = "nvarchar(255)")]
        public string? Notes { get; set; }

        [Column(nameof(CancelledAt), TypeName = "datetime2(7)")]
        public DateTime? CancelledAt { get; set; }

        [Column(nameof(CompletedAt), TypeName = "datetime2(7)")]
        public DateTime? CompletedAt { get; set; }
    }
}
