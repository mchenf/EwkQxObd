using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent an Account
    /// </summary>
    /// 
    [Table("Account", Schema = "eqo")]
    public class EqoAccount
    {
        public int Id { get; set; }
        public int PartnerId { get; set; } = int.MinValue;
        public string PartnerName { get; set; } = string.Empty;
        public Guid GeisID { get; set; }

        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        //public List<EqoContactInfo>? OrgnisationAdmins { get; set; }

    }
}
