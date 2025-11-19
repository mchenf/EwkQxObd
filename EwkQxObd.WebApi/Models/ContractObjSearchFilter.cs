namespace EwkQxObd.WebApi.Models
{
    public class ContractObjSearchFilter
    {
        public long ContractNumber { get; set; } = long.MinValue;
        public long PartnerAccountNumber { get; set; } = long.MinValue;
        public string SerialNumber { get; set; } = string.Empty;
        public string System { get; set; } = "Pacific";
        public long NetworkID { get; set; } = long.MinValue;

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
                if (NetworkID != long.MinValue)
                {
                    ls |= ContractObjSearchTermLoadState.NetworkID;
                }
                if (!string.IsNullOrEmpty(System))
                {
                    ls |= ContractObjSearchTermLoadState.System;
                }
                return ls;
            }
        }
    }
}
