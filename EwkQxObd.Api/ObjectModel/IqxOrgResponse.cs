using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Api.ObjectModel
{
    public class IqxOrgResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = default;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("accountNumber")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("street")]
        public string Street { get; set; } = string.Empty;

        [JsonPropertyName("isSite")]
        public bool IsSite { get; set; } = false;

        [JsonPropertyName("audience")]
        public string Audience { get; set; } = string.Empty;
    }
}
