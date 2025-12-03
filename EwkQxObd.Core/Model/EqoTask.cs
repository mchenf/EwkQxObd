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
        [Column(nameof(Name), TypeName = "nvarchar(16)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(nameof(Description), TypeName = "nvarchar(255)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(nameof(WorkFlow), TypeName = "int")]
        public int WorkFlowId { get; set; }
        public EqoTaskWorkflow? WorkFlow { get; set; }

        [Column(nameof(Prerequisite), TypeName = "int")]
        public int? PrerequisiteId { get; set; }
        public EqoTask? Prerequisite { get; set; }

    }
}
