namespace EwkQxObd.WebApi.Data
{
    [Flags]
    public enum ContractObjectDataMissing
    {
        AllGreen = 0b_0000_0000_0000_0000,
        ShipTo = 0b_0000_0000_0000_0001,
        Contract = 0b_0000_0000_0000_0010
    }
}