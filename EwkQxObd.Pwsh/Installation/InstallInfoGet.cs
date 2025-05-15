using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Installation
{
    [Cmdlet(VerbsCommon.Get, "InstallInfo")]
    public class InstallInfoGet : InstallBase
    {
        protected override void ProcessRecord()
        {
            printAll();
        }

        protected private void printAll()
        {
            WriteObject(appRootDir);
            WriteObject(appDbDir);
        }
    }
}
