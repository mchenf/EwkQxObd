using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableProduct
{
    public class EqoCreateTblProduct : EqoDataStoreBase
    {
        private const string Cmdtx_C_Tbl_Contract = @"
CREATE TABLE IF NOT EXISTS Eqo_Product (
    Id              INTEGER PRIMARY KEY     AUTOINCREMENT,
    SerialNumber    UNSIGNED BIG INT        NOT NULL,
    ValidFrom       DATE                    NOT NULL,
    ValidTo         DATE                    NOT NULL,
    FOREIGN KEY     (ContractId)    REFERENCES Eqo_Contract(Id),
    FOREIGN KEY     (ShipToId)      REFERENCES Eqo_Account(Id),
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
