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
    [Table("User", Schema = "iqx")]
    public class User
    {
        [Key]
        [JsonPropertyName("userGuid")]
        [Column(nameof(UserGuid), TypeName = "uniqueidentifier")]
        public Guid UserGuid { get; set; }

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

        public User ShallowCopy()
        {
            return (User)MemberwiseClone();
        }

    }
}
