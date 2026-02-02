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
    /// Represent an Account
    /// </summary>
    /// 
    [Obsolete("Use IqxOrganization Instead.")]
    [Table("Account", Schema = "eqo")]
    public class EqoAccount
    {
        [Key]
        public int Id { get; set; }

        [Column(nameof(PartnerId), TypeName = "int")]
        public int PartnerId { get; set; } = int.MinValue;

        [Column(nameof(PartnerName), TypeName = "nvarchar(64)")]
        public string PartnerName { get; set; } = string.Empty;

        [Column(nameof(GeisID), TypeName = "uniquidentifier")]
        public Guid? GeisID { get; set; }


        [Column(nameof(Country), TypeName = "nvarchar(16)")]
        public string Country { get; set; } = string.Empty;

        [Column(nameof(Region), TypeName = "nvarchar(16)")]
        public string Region { get; set; } = string.Empty;

        //public List<EqoContactInfo>? OrgnisationAdmins { get; set; }

    }
}
