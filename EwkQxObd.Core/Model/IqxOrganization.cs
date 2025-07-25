using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public class IqxOrganization
    {
        [JsonPropertyName("id")]
        public Guid GeisGuid { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("accountNumber")]
        public string AccountNumber { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
        [JsonPropertyName("street")]
        public string Street { get; set; } = string.Empty;

    }
}
