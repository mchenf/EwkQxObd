using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("Task", Schema = "eqo")]
    public class EqoTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(nameof(TaskDef), TypeName = "int")]
        public int TaskDef { get; set; }


        [Column(nameof(Ticket), TypeName = "int")]
        public int Ticket { get; set; }

        [Column(nameof(Notes), TypeName = "nvarchar(255)")]
        public string Notes { get; set; } = string.Empty;

        [Required]
        [Column(nameof(CreatedAt), TypeName = "datetime2(7)")]
        public DateTime CreatedAt { get; set; }

        [Column(nameof(CancelledAt), TypeName = "datetime2(7)")]
        public DateTime CancelledAt { get; set; }

        [Column(nameof(CompletedAt), TypeName = "datetime2(7)")]
        public DateTime CompletedAt { get; set; }

    }
}
