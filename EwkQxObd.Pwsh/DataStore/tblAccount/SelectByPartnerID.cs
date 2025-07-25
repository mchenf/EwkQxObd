using EwkQxObd.Core.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.DataStore.tblAccount
{
    public class SelectByPartnerID : LocalStoreBase
    {
        

        private const string cmmandStr1 = @"
USE EwIqxOnboarding; 
SELECT 
    * 
FROM eqoAccount 
WHERE 
    PartnerID = @partnerID;
";

        public EqoAccount SelectTable(long PartnerID)
        {
            throw new NotImplementedException();
        }
    }
}
