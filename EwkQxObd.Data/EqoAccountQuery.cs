using Microsoft.Data.Sqlite;

namespace EwkQxObd.Data
{
    public class EqoAccountQuery
    {
        public EqoAccountQuery()
        {
            
        }

        public void Connect()
        {
            using (var conn = new SqliteConnection("Data Source=hello.db"))
            {
                conn.Open();

                var command = conn.CreateCommand();

                command.CommandText =
@"
    SELECT name
    FROM user
    WHERE id = $id
";
                command.Parameters.AddWithValue("$id", 3);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        Console.WriteLine(name);
                    }
                }
            }
        }
    }
}
