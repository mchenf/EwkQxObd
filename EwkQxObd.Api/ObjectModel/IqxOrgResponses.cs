using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Api.ObjectModel
{
    public class IqxOrgResponses
    {
        public required IEnumerable<IqxOrgResponses> Responses { get; set; }
    }
}
