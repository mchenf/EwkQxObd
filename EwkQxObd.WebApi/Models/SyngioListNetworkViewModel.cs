using EwkQxObd.Core.Model.Views;

namespace EwkQxObd.WebApi.Models
{
    public class SyngioListNetworkViewModel
    {
        public required List<SyngioViewNetwork> Networks { get; set; }
        public required List<SyngioSearchAlpha> SearchAlpha { get; set; }
    }
}
