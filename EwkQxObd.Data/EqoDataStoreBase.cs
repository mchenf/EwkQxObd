using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data
{
    public class EqoDataStoreBase : IDisposable
    {
        internal static readonly string ConnectionStr
            = "Data Source=EwkQxObd.Main.DataStore.db";


        internal static SqliteConnection Connection
            = new SqliteConnection(ConnectionStr);
        public void Dispose()
        {
            Connection.Dispose();
        }

        internal void OpenConnDoStuff(Action stuffToDo)
        {
            Connection.Open();
            stuffToDo.Invoke();
            Connection.Close();
        }
    }
}
