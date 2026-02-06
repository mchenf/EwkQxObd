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
    [Table("Org", Schema = "iqx")]
    [Index(nameof(GeisGuid), IsUnique = true)]
    public class IqxOrganization
    {
        [Key]
        [JsonPropertyName("accountNumber")]
        [Column(nameof(AccountNumber), TypeName = "int")]
        public int AccountNumber { get; set; } = int.MinValue;

        [JsonPropertyName("id")]
        [Column(nameof(GeisGuid), TypeName = "uniqueidentifier")]
        public Guid? GeisGuid { get; set; }

        [JsonPropertyName("name")]
        [Column(nameof(Name), TypeName = "nvarchar(64)")]
        public string? Name { get; set; } = string.Empty;


        [JsonPropertyName("region")]
        [Column(nameof(Region), TypeName = "nvarchar(16)")]
        public string? Region { get; set; } = string.Empty;


        [JsonPropertyName("country")]
        [Column(nameof(Country), TypeName = "nvarchar(16)")]
        public string? Country { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        [Column(nameof(City), TypeName = "nvarchar(64)")]
        public string? City { get; set; } = string.Empty;

        [JsonPropertyName("street")]
        [Column(nameof(Street), TypeName = "nvarchar(128)")]
        public string? Street { get; set; } = string.Empty;

    }
}
