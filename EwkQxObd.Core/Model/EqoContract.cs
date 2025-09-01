using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public long Id { get; set; }
        public long ContractNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public EqoContactInfo? CustomerContact { get; set; }
        [Column("CustomerContact")]
        public long? CustomerContactId { get; set; }

        public EqoContactInfo? EmployeeResponsible { get; set; }
        [Column("EmployeeResponsible")]
        public long? EmployeeResponsibleId { get; set; }
    }
}
