using EwkQxObd.Data.TableContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Installation
{
    [Cmdlet(VerbsLifecycle.Install, "EwkQxObd")]
    public class InstallInitialize : InstallBase
    {
        private bool should_Terminate = false;

        public InstallInitialize()
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
                WriteWarning($"{nameof(InstallInitialize)}~:: Failed to pass initial check.");
                WriteWarning($"{nameof(InstallInitialize)}~:: Maybe already installed.");
                return;
            }
            WriteVerbose("Beginning Installation");

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
