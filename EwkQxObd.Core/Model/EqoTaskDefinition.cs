using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("TaskDefinition", Schema = "eqo")]
    public class EqoTaskDefinition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(nameof(Name), TypeName = "nvarchar(32)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(nameof(Description), TypeName = "nvarchar(255)")]
        public string Description { get; set; } = string.Empty;

    }
}
