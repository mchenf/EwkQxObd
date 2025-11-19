namespace EwkQxObd.WebApi.Models
{
    [Flags]
    public enum ContractObjSearchTermLoadState
    {
        None = 0,
        ContractNumber = 0b_0000_0000_0000_0001,
        PartnerAccountNumber = 0b_0000_0000_0000_0010,
        SerialNumber = 0b_0000_0000_0000_0100,
        System = 0b_0000_0000_0000_1000,
        NetworkID = 0b_0000_0000_0001_0000,
    }
}
