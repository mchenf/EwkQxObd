using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public class EqoCreateTblContract : EqoDataStoreBase
    {
        private const string Cmdtx_C_Tbl_Contract = @"
CREATE TABLE IF NOT EXISTS Eqo_Contract (
    Id              INTEGER PRIMARY KEY     AUTOINCREMENT,
    ContractNo      UNSIGNED BIG INT    NOT NULL,
    ValidFrom       DATE                NOT NULL,
    ValidTo         DATE                NOT NULL
);
";
        public void CreateTable()
        {
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_C_Tbl_Contract;
                command.ExecuteNonQuery();
            });
        }
    }
}
