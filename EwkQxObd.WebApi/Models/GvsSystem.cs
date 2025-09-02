namespace EwkQxObd.WebApi.Models
{
    public class GvsSystem
    {

        public string SystemName { get; set; } = string.Empty;
        public List<GvsNetwork>? Networks { get; set; }
    }
}