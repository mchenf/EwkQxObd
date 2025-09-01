namespace EwkQxObd.WebApi.Models
{
    public class ContractObjSearchFilter
    {
        public long ContractNumber { get; set; } = long.MinValue;
        public string PartnerAccountNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

    }
}
