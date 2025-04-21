using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Task
{
    public class EqoLinkTask
    {
        public IqxNetwork Network { get; set; }
        public EqoAccount? Before { get => Network.Account; }
        public EqoAccount After { get; set; }
        public DateOnly StatusUpdated { get; set; }

        public EqoLinkTask(IqxNetwork network, EqoAccount LinkTo)
        {
            Network = network;
            After = LinkTo;
        }
    }
}
