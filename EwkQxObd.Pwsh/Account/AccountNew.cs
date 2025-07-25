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
    [Cmdlet(VerbsCommon.New, "Account")]
    public class AccountNew : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public long PartnerID { get; set; } = long.MinValue;

        [Parameter(Mandatory = true, Position = 1)]
        public string PartnerName { get; set; } = string.Empty;



        [Parameter(Mandatory = false, Position = 3)]
        public Guid GeisGuid { get; set; } = Guid.Empty;

        [Parameter(Mandatory = false, Position = 4)]
        public string Region { get; set; } = string.Empty;

        [Parameter(Mandatory = false, Position = 5)]
        public string Country { get; set; } = string.Empty;


        protected override void ProcessRecord()
        {
            using var query = new InsertIntoValues();


            throw new NotImplementedException();


        }
    }
}
