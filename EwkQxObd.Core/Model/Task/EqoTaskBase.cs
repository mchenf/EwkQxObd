using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Task
{
    /// <summary>
    /// A Task is something to be completed.
    /// </summary>
    public abstract class EqoTaskBase
    {
        public EqoTaskBase? Prerequisite { get; set; }
        public IEnumerable<EqoBlocker>? Blockers { get; set; }
    }
}
