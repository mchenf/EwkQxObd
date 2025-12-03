namespace EwkQxObd.WebApi.Models
{
    public class ContractObjSearchFilter
    {
        public int ContractNumber { get; set; } = int.MinValue;
        public int PartnerAccountNumber { get; set; } = int.MinValue;
        public string SerialNumber { get; set; } = string.Empty;
        public string System { get; set; } = "Pacific";
        public int NetworkID { get; set; } = int.MinValue;

        public ContractObjSearchTermLoadState LoadState
        {
            get
            {
                ContractObjSearchTermLoadState ls = ContractObjSearchTermLoadState.None;

                if (ContractNumber != int.MinValue)
                {
                    ls |= ContractObjSearchTermLoadState.ContractNumber;
                }
                if (PartnerAccountNumber != int.MinValue)
                {
                    ls |= ContractObjSearchTermLoadState.PartnerAccountNumber;
                }
                if (!string.IsNullOrEmpty(SerialNumber))
                {
                    ls |= ContractObjSearchTermLoadState.SerialNumber;
                }
                if (NetworkID != int.MinValue)
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
