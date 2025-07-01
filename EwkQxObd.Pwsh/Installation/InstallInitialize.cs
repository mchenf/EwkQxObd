using EwkQxObd.Data.TableContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Installation
{
    [Cmdlet(VerbsData.Initialize, "Install")]
    public class InstallInitialize : InstallBase
    {
        private bool should_Terminate = false;

        public InstallInitialize()
        {
            
        }

        protected override void BeginProcessing()
        {
            var installedStat = EqoInstallStatus.None;

            installedStat = Path.Exists(appDbDir) ? 
                installedStat | EqoInstallStatus.DbFileExist 
                : installedStat | EqoInstallStatus.None;

            if (installedStat == EqoInstallStatus.Installed)
            {
                should_Terminate = true;
            }
        }

        protected override void ProcessRecord()
        {
            if (should_Terminate)
            {
                WriteWarning($"{nameof(InstallInitialize)}~:: Failed to pass initial check.");
                WriteWarning($"{nameof(InstallInitialize)}~:: Maybe already installed.");
                return;
            }
            WriteInformation(new InformationRecord(
                "Begin installation",
                nameof(InstallInitialize)
            ));

            using EqoCreateTblContract storeContract = new();

            storeContract.CreateTable();



        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
        }
    }
}
