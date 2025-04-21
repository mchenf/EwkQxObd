using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public class IqxNetwork
    {
        public IqxSystem System { get; set; }
        public Guid NetworkGuid { get; set; }
        public EqoAccount? Account { get; set; }

        public IqxNetwork(IqxSystem system)
        {
            System = system;
        }
    }
}
