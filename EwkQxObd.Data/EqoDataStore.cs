using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data
{
    public class EqoDataStore : IDisposable
    {
        private static readonly string ConnectionStr 
            = "Data Source=EwkQxObd.Main.DataStore.db";

        private static readonly string Cmdtx_C_Tbl_Contract
= @"
CREATE TABLE IF NOT EXISTS Eqo_Contract (
    Id              INT PRIMARY KEY     ,
    ContractNo      UNSIGNED BIG INT    NOT NULL,
    ValidFrom       DATE                NOT NULL,
    ValidTo         DATE                NOT NULL
);
";
        private static readonly string Cmdtx_Drp_Tbl_Contract
= @"
DROP TABLE IF EXISTS Eqo_Contract;
";
        private static readonly string Cmdtx_I_Tbl_Contract
= @"
INSERT INTO Eqo_Contract (
    ContractNo,
    ValidFrom,
    ValidTo
)
VALUES (
    $ContractNo,
    $ValidFrom,
    $ValidTo
);
";


        private static SqliteConnection Connection
            = new SqliteConnection(ConnectionStr);

        public EqoDataStore()
        {
             
        }

        private void openConnDoStuff(Action stuffToDo)
        {
            Connection.Open();
            stuffToDo.Invoke();
            Connection.Close();
        }

        public void DropTable()
        {
            openConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_Drp_Tbl_Contract;
                command.ExecuteNonQuery();
            });
        }

        public void CreateTables()
        {
            openConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_C_Tbl_Contract;
                command.ExecuteNonQuery();
            });
        }

        public int InsertContracts(
            uint ContractNo,
            DateOnly ValidFrom,
            DateOnly ValidTo)
        {
            //HACK: Maybe put some checks here, like compaison b/w from and to
            int rowsAffected = -1;
            openConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_I_Tbl_Contract;

                command.Parameters.AddWithValue("$ContractNo", ContractNo);
                command.Parameters.AddWithValue("$ValidFrom", ValidFrom);
                command.Parameters.AddWithValue("$ValidTo", ValidTo);

                rowsAffected = command.ExecuteNonQuery();
            });

            return rowsAffected;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
