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
        public required User User { get; set; }

        [JsonPropertyName("email")]
        [Column(nameof(Email), TypeName = "nvarchar(50)")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        [Column(nameof(FirstName), TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        [Column(nameof(LastName), TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("locked")]
        [Column(nameof(Locked), TypeName = "bit")]
        public bool Locked { get; set; }

        [JsonPropertyName("organization")]
        [Column(nameof(OrgGuid), TypeName = "uniqueidentifier")]
        public Guid OrgGuid { get; set; }

        [JsonPropertyName("queriedAt")]
        [Column(nameof(QueriedAt), TypeName = "datetime2(7)")]
        public DateTime QueriedAt { get; set; }


        public UserOrganization ShallowCopy()
        {
            return (UserOrganization)MemberwiseClone();
        }
    }
}
