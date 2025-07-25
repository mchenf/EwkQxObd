using EwkQxObd.Core.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.DataStore.tblAccount
{
    public class InsertIntoValues : LocalStoreBase
    {


        private const string insertCommand = @"
USE EwIqxOnboarding; 
INSERT INTO eqoAccount (
    PartnerID,
    PartnerName,
    GeisGuid,
    Region,
    Country
) VALUES (
    @val1,
    @val2,
    @val3,
    @val4,
    @val5
);";

        public EqoAccount SelectTable(EqoAccount newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
