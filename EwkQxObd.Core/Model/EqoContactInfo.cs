using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("ContactInfo", Schema = "eqo")]
    public class EqoContactInfo
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
