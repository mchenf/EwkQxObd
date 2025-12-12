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
    /// <summary>
    /// Represent a Contract object
    /// </summary>
    /// 
    [Table("Contract", Schema = "eqo")]
    [Index(nameof(ContractNumber), IsUnique = true)]
    public class EqoContract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(nameof(ContractNumber), TypeName = "int")]
        [Range(999, 99999999, ErrorMessage = "Not a valid contract number")]
        public int ContractNumber { get; set; }

        [Required]
        [Column(nameof(Description), TypeName = "nvarchar(255)")]
        [StringLength(255, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;


        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public EqoContactInfo? CustomerContact { get; set; }
        [Column("CustomerContact")]
        public int? CustomerContactId { get; set; }

        public EqoContactInfo? EmployeeResponsible { get; set; }
        [Column("EmployeeResponsible")]
        public int? EmployeeResponsibleId { get; set; }


        public DateTime? RecordedAt { get; set; }
    }
}
