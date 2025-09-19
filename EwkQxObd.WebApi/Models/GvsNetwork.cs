using EwkQxObd.Core.Model.Views;

namespace EwkQxObd.WebApi.Models
{
    public class GvsNetwork
    {
        public long? NetworkId { get; set; }
        public string NetworkName { get; set; } = string.Empty;
        public List<Syngio>? Instruments { get; set; }
    }
}