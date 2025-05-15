using System.Management.Automation;

namespace EwkQxObd.Pwsh
{
    [Cmdlet(VerbsData.Initialize, "Installation")]
    public class InstallationInitialize : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteDebug("InstallationInitialize starts");



        }
    }
}
