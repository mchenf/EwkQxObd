using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("Organization", Schema = "iqx")]
    [Index(nameof(GeisGuid), IsUnique = true)]
    public class IqxOrganization
    {
        [Key]
        [JsonIgnore]
        public int id { get; set; }


        [JsonPropertyName("id")]
        [Column(nameof(GeisGuid), TypeName = "uniqueidentifier")]
        public Guid GeisGuid { get; set; }

        [JsonPropertyName("name")]
        [Column(nameof(Name), TypeName = "nvarchar(64)")]
        public string Name { get; set; } = string.Empty;


        [JsonPropertyName("accountNumber")]
        [Column(nameof(AccountNumber), TypeName = "int")]
        public int AccountNumber { get; set; } = int.MinValue;

        [JsonPropertyName("city")]
        [Column(nameof(City), TypeName = "nvarchar(64)")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("street")]
        [Column(nameof(Street), TypeName = "nvarchar(128)")]
        public string? Street { get; set; } = string.Empty;

    }
}
