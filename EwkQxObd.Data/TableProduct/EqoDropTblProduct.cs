using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableProduct
{
    public class EqoDropTblProduct : EqoDataStoreBase
    {
        private const string Cmdtx_Drp_Tbl_Product
= @"
DROP TABLE IF EXISTS Eqo_Product;
";
        public void DropTable()
        {
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_Drp_Tbl_Product;
                command.ExecuteNonQuery();
            });
        }
    }
}
