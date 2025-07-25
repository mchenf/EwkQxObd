using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public class IqxNetwork
    {
        public long id { get; set; }
        public IqxSystem? System { get; set; }
        public Guid NetworkGuid { get; set; }
        public EqoAccount? Account { get; set; }

    }
}
