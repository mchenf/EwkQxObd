using EwkQxObd.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.DataStore
{
    public abstract class LocalStoreBase : IDisposable
    {
        internal static string ConnectionStr = "Server=localhost\\SQLEXPRESS;Integrated Security=true;Encrypt=no";


        internal static SqlConnection Connection = new(ConnectionStr);

        public LocalStoreBase()
        {

        }

        public void Dispose()
        {
            Connection!.Dispose();
        }

        private protected void OpenConnDoStuff(Action stuffToDo)
        {
            Connection.Open();
            stuffToDo.Invoke();
            Connection.Close();
        }

        private protected T OpenConnGetStuff<T>(Func<T> stuffToGet)
        {
            
            Connection.Open();
            T result = stuffToGet.Invoke();
            Connection.Close();
            return result;
        }

        private protected EqoAccount SelectAccountById(long PartnerID, string commandStr)
        {
            EqoAccount result = new();

            var command = Connection.CreateCommand();
            command.CommandText = commandStr;
            command.Parameters.AddWithValue("partnerID", PartnerID);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                var cols = reader.GetColumnSchema();
                reader.Read();

#if DEBUG
                foreach (var c in cols)
                {
                    Console.Write(c.ColumnName);
                    Console.Write(new string('\t', 5));
                    Console.Write(c.DataType);
                    Console.Write(new string('\t', 3));
                    Console.Write(c.ColumnOrdinal);
                    Console.WriteLine();
                }
#endif
                result.PartnerId = reader.GetInt64(0);
                result.PartnerName = reader.GetString(1);

                if (!reader.IsDBNull(2))
                {
                    result.GeisID = reader.GetGuid(2);
                }
                result.Id = reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                {
                    result.Country = reader.GetString(4);
                }

                if (!reader.IsDBNull(5))
                {
                    result.Region = reader.GetString(5);
                }
            }


            reader.Close();

            return result;
        }

    }
}
