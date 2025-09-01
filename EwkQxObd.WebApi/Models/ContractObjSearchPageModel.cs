using EwkQxObd.Core.Model;

namespace EwkQxObd.WebApi.Models
{
    public class ContractObjSearchPageModel
    {
        public ContractObjSearchFilter? FilterApplied { get; set; } = null;
        public List<EqoContractObject>? Results { get; set; } = null;
    }
}
