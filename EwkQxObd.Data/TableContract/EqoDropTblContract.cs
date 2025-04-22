using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public class EqoDropTblContract : EqoDataStoreBase
    {
        private const string Cmdtx_Drp_Tbl_Contract
= @"
DROP TABLE IF EXISTS Eqo_Contract;
";
        public void DropTable()
        {
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_Drp_Tbl_Contract;
                command.ExecuteNonQuery();
            });
        }
    }
}
