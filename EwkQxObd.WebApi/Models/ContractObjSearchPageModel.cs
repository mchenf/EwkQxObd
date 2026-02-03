using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Views;

namespace EwkQxObd.WebApi.Models
{
    public class ContractObjSearchPageModel
    {
        public ContractObjSearchFilter? FilterApplied { get; set; } = null;
        public List<InstrumentLinkStatus>? Results { get; set; } = null;
    }
}
