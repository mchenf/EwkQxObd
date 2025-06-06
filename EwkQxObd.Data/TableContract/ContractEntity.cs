﻿using EwkQxObd.Core.Model;
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

            contract.Id = reader.GetInt64(0);
            contract.ContractNumber = reader.GetInt64(1);
            contract.Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            contract.ValidFrom = reader.GetDateTime(3);
            contract.ValidTo = reader.GetDateTime(4);


            return contract;
        }

        private const string PrintTemplate =
            "{0}\t\t{1}\t\t{2}\t{3}";

        public static void Cout(this EqoContract contract)
        {
            Console.WriteLine(PrintTemplate,
                contract.Id,
                contract.ContractNumber,
                contract.ValidFrom,
                contract.ValidTo);
        }

    }
}
