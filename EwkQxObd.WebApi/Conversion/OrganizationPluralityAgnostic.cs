using EwkQxObd.Core.Model;
using System.Text.Json.Serialization;

namespace EwkQxObd.WebApi.Conversion
{
    public class OrganizationPluralityAgnostic
    {
        [JsonConverter(typeof(JsonSingleArrayConverter<IqxOrganization>))]
        public List<IqxOrganization>? Organizations { get; set; }
    }
}
