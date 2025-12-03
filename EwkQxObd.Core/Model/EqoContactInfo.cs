using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    
    [Table("ContactInfo", Schema = "eqo")]
    [Index(nameof(EmailAddress), IsUnique = true)]
    public class EqoContactInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(nameof(FullName), TypeName = "nvarchar(32)")]
        [StringLength(32, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string FullName { get; set; } = string.Empty;
        [Column(nameof(EmailAddress), TypeName = "nvarchar(64)")]
        [StringLength(64, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string? EmailAddress { get; set; } = string.Empty;
    }
}
