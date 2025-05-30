using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public class EqoInsertTblProduct : EqoDataStoreBase
    {
        private const string Cmdtx_I_Tbl_Product
= @"
INSERT INTO Eqo_Product (
    ContractId,
    SerialNumber,
    ValidTo
)
VALUES (
    @ContractNo,
    @ValidFrom,
    @ValidTo
);
";
        public int InsertProduct(
            long ContractNo,
            DateOnly ValidFrom,
            DateOnly ValidTo)
        {
            //HACK: Maybe put some checks here, like compaison b/w from and to
            int rowsAffected = -1;
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_I_Tbl_Product;

                command.Parameters.Add("@ContractNo", SqliteType.Integer);
                command.Parameters[0].Value = ContractNo;
                command.Parameters.AddWithValue("@ValidFrom", ValidFrom);
                command.Parameters.AddWithValue("@ValidTo", ValidTo);

                rowsAffected = command.ExecuteNonQuery();
            });

            return rowsAffected;
        }
    }
}
