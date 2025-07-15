using EwkQxObd.Core.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.Contract
{
    [Cmdlet(VerbsCommon.Get, "Account")]
    public class AccountGet : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByPartnerID")]
        public long? PartnerID { get; set; }

        private const string connStr = "Server=localhost\\SQLEXPRESS;Integrated Security=true;Encrypt=no";
        private const string cmmandStr1 = "USE EwIqxOnboarding; SELECT * FROM eqoAccount WHERE PartnerID = @partnerID;";




        protected override void ProcessRecord()
        {


            EqoAccount result = new();

            using (SqlConnection conn = new(connStr))
            {
                conn.Open();

                using SqlCommand command = new(cmmandStr1, conn) ;

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
                conn.Close();

                WriteObject(result);
            }
        }
    }
}
