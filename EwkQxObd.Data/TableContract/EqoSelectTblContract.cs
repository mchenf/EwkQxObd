using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public class EqoSelectTblContract : EqoDataStoreBase
    {
        private const string Cmdtx_SLCT_Tbl_Contract
= @"
SELECT * FROM Eqo_Contract;
";
        public int SelectAll()
        {
            int rowsAffected = -1;
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();

                command.CommandText = Cmdtx_SLCT_Tbl_Contract;

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var entity = reader.ToEqoContract();

                    entity.Cout();

                }


            });

            return rowsAffected;
        }
    }
}
