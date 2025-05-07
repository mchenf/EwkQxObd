using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableAccount
{
    public class EqoCreateTblAccount
        : EqoDataStoreBase
    {
        private const string Cmdtx_C_Tbl_Account = @"
CREATE TABLE IF NOT EXISTS Eqo_Account (
    Id              INTEGER PRIMARY KEY     AUTOINCREMENT,
    PartnerName     TEXT                NOT NULL,
    GeisGuid        BLOB
);
";
        public void CreateTable()
        {
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_C_Tbl_Account;
                command.ExecuteNonQuery();
            });
        }
    }
}
