using EwkQxObd.Core.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public static class ContractEntity
    {
        public static EqoContract ToEqoContract(
            this SqliteDataReader reader)
        {
            EqoContract contract = new EqoContract();

            
            contract.ContractNumber = reader.GetInt64(1);
            contract.ValidFrom = reader.GetDateTime(2);
            contract.ValidTo = reader.GetDateTime(3);


            return contract;
        }

        private const string PrintTemplate =
            "{0}\t\t{1}\t{2}";

        public static void Cout(this EqoContract contract)
        {
            Console.WriteLine(PrintTemplate,
                contract.ContractNumber,
                contract.ValidFrom,
                contract.ValidTo);
        }

    }
}
