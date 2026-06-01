namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.Common
{
    public record ContractObjectFilter
    {
        public string SerialNumber { get; set; } = string.Empty;
        public int? InstrumentType { get; set; }
        public int? ContractNumber { get; set; }
        public int? ShipToNumber { get; set; }

    }
}
