using EwkQxObd.Data.TableContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Installation
{
    [Cmdlet(VerbsLifecycle.Install, "EwkIqxObd")]
    public class EwkIqxObd_Install : InstallBase
    {
        private bool should_Terminate = false;

        public EwkIqxObd_Install()
        {
            
        }

        protected override void BeginProcessing()
        {
            WriteVerbose("Beginning Pre-install checks.");
            var installedStat = EqoInstallStatus.None;


            WriteVerbose("Checking if db file exists");
            installedStat = Path.Exists(appDbDir) ? 
                installedStat | EqoInstallStatus.DbFileExist 
                : installedStat | EqoInstallStatus.None;

            if (installedStat == EqoInstallStatus.Installed)
            {

                WriteVerbose("Already installed");
                should_Terminate = true;
            }
            else
            {
                WriteVerbose("Not installed.");
            }
        }

        protected override void ProcessRecord()
        {
            if (should_Terminate)
            {
                WriteWarning($"{nameof(EwkIqxObd_Install)}~:: Failed to pass initial check.");
                WriteWarning($"{nameof(EwkIqxObd_Install)}~:: Maybe already installed.");
                return;
            }
            WriteVerbose("Beginning Installation");

            if (!Directory.Exists(appRootDir))
            {
                WriteVerbose("No root folder detected, creating folder");

                Directory.CreateDirectory(appRootDir);

            }

            WriteVerbose("Creating QueryStore for Contracts");
            using EqoCreateTblContract storeContract = new(appDbDir);

            storeContract.CreateTable();

            WriteVerbose("QueryStore for Contracts Created");

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
