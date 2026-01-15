namespace EwkQxObd.WebApi.ModelsPage.ContractObject
{
    public class CojtIndexFilter
    {
        public string ShipToSearchText { get; set; } = string.Empty;
        public bool IsHideOnboarded { get; set; } = false;
        public int? Contract { get; set; }
        public int? SerialNumber { get; set; }
        public int? ShipToNumber { get; set; }

    }
}
