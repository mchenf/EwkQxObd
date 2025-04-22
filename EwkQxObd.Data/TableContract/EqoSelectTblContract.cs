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
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var t = reader.GetFieldType(i);
                        Console.WriteLine($"Type at {i} is {t.FullName}");
                    }
                }


            });

            return rowsAffected;
        }
    }
}
