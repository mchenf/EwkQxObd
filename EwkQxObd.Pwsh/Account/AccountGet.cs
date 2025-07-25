using EwkQxObd.Core.Model;
using EwkQxObd.Pwsh.DataStore.tblAccount;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Contract
{
    [Cmdlet(VerbsCommon.Get, "Account")]
    public class AccountGet : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByPartnerID")]
        public long PartnerID { get; set; }


        protected override void ProcessRecord()
        {
            using var query = new SelectByPartnerID();

            var result = query.SelectTable(PartnerID);
            WriteObject(result);
        }
    }
}
