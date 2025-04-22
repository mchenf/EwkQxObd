using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public class EqoInsertTblContract : EqoDataStoreBase
    {
        private const string Cmdtx_I_Tbl_Contract
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
        public int InsertContracts(
            uint ContractNo,
            DateOnly ValidFrom,
            DateOnly ValidTo)
        {
            //HACK: Maybe put some checks here, like compaison b/w from and to
            int rowsAffected = -1;
            OpenConnDoStuff(() =>
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
    }
}
