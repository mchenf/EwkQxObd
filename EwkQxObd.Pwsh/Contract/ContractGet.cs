using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Contract
{
    [Cmdlet(VerbsCommon.Get, "Contract")]
    public class ContractGet : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string? ContractNumber { get; set; }

        private const string connStr = "Server=localhost\\SQLEXPRESS;Integrated Security=true;Encrypt=no";
        private const string cmmandStr = "USE EwIqxOnboarding; SELECT * FROM ContractDetail_GEIS_Explicit;";





        protected override void ProcessRecord()
        {
            using(SqlConnection conn = new(connStr))
            {
                conn.Open();

                using SqlCommand command = new(cmmandStr, conn) ;

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    WriteError(new(
                        new InvalidDataException("Query returns no data"),
                        "3317",
                        ErrorCategory.InvalidData,
                        this
                        ));
                    return;
                }

                var cols = reader.GetColumnSchema();

                foreach (var c in cols)
                {
                    Console.Write(c.ColumnName);
                    Console.Write(new string('\t', 5));
                    Console.Write(c.DataType);
                    Console.Write(new string('\t', 3));
                    Console.Write(c.ColumnOrdinal);
                    Console.WriteLine();
                }






                reader.Close();
                conn.Close();
            }
        }
    }
}
