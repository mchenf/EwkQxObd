using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Iqx
{
    [Table("UserOrganization", Schema ="iqx")]
    public class UserOrganization
    {
        [Key]
        [Column(nameof(Id), TypeName = "int")]
        public int Id { get; set; }

        [JsonIgnore]
        [Column(nameof(UserGuid), TypeName = "uniqueidentifier")]
        public Guid UserGuid { get; set; }

        [JsonPropertyName("user")]
        [NotMapped]
        public required User User { get; set; }
         
        [JsonIgnore]
        [Column(nameof(OrgAccountNo), TypeName = "int")]
        public int OrgAccountNo { get; set; }


        [JsonPropertyName("organization")]
        [NotMapped]
        public required Organization Organization { get; set; }

        [JsonPropertyName("queriedAt")]
        [Column(nameof(QueriedAt), TypeName = "datetime2(7)")]
        public DateTime QueriedAt { get; set; }


        public UserOrganization ShallowCopy()
        {
            return (UserOrganization)MemberwiseClone();
        }
    }
}
