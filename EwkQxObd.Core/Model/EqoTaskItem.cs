using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("TaskItem", Schema = "eqo")]
    public class EqoTaskItem
    {
        [Key]
        public int Id { get; set; }

        [Column(nameof(Task), TypeName = "int")]
        public int? TaskId { get; set; }
        public EqoTask? Task { get; set; }


    }
}
