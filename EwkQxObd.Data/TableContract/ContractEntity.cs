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
        public static EqoContract? ToEqoContract(
            this SqliteDataReader reader)
        { 
            throw new NotImplementedException();
        }

    }
}
