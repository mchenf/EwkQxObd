namespace EwkQxObd.WebApi.Models
{
    public class ContractObjSearchFilter
    {
        public long ContractNumber { get; set; } = long.MinValue;
        public long PartnerAccountNumber { get; set; } = long.MinValue;
        public string SerialNumber { get; set; } = string.Empty;

        public ContractObjSearchTermLoadState LoadState
        {
            get
            {
                ContractObjSearchTermLoadState ls = ContractObjSearchTermLoadState.None;

                if (ContractNumber != long.MinValue)
                {
                    ls |= ContractObjSearchTermLoadState.ContractNumber;
                }
                if (PartnerAccountNumber != long.MinValue)
                {
                    ls |= ContractObjSearchTermLoadState.PartnerAccountNumber;
                }
                if (!string.IsNullOrEmpty(SerialNumber))
                {
                    ls |= ContractObjSearchTermLoadState.SerialNumber;
                }

                return ls;
            }
        }
    }
}
