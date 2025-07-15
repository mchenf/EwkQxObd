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
    [Cmdlet(VerbsCommon.New, "Account")]
    public class AccountNew : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public long PartnerID { get; set; } = long.MinValue;

        [Parameter(Mandatory = true, Position = 1)]
        public string PartnerName { get; set; } = string.Empty;



        [Parameter(Mandatory = false, Position = 3)]
        public Guid GeisGuid { get; set; } = Guid.Empty;

        [Parameter(Mandatory = false, Position = 4)]
        public string Region { get; set; } = string.Empty;

        [Parameter(Mandatory = false, Position = 5)]
        public string Country { get; set; } = string.Empty;


        private const string connStr = "Server=localhost\\SQLEXPRESS;Integrated Security=true;Encrypt=no";
        private const string insertCommand = 
            @"
USE EwIqxOnboarding; 
INSERT INTO eqoAccount (
PartnerID,
PartnerName,
GeisGuid,
Region,
Country
) VALUES (
@val1,
@val2,
@val3,
@val4,
@val5
);";




        protected override void ProcessRecord()
        {
            

            using(SqlConnection conn = new(connStr))
            {
                conn.Open();

                using SqlCommand command = new(insertCommand, conn) ;

                command.Parameters.AddWithValue("@val1", PartnerID);
                command.Parameters.AddWithValue("@val2", PartnerName);
                command.Parameters.AddWithValue("@val3", GeisGuid);
                command.Parameters.AddWithValue("@val4", Region);
                command.Parameters.AddWithValue("@val5", Country);

                var rowsAffected = command.ExecuteNonQuery();

                

                conn.Close();
            }
        }
    }
}
