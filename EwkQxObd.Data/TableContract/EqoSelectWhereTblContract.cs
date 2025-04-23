using EwkQxObd.Core.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Data.TableContract
{
    public class EqoSelectWhereTblContract : EqoDataStoreBase
    {
        private const string Cmdtx_SLCT_byID_Tbl_Contract
= @"
SELECT * FROM Eqo_Contract WHERE Id = @Id;
";
        private const string Cmdtx_SLCT_byCntr_Tbl_Contract
= @"
SELECT * FROM Eqo_Contract WHERE ContractNo = @Contract;
";

        private EqoContract? SelectByBase(string Parm, object Val)
        {
            EqoContract? res = null;
            OpenConnDoStuff(() =>
            {
                var command = Connection.CreateCommand();
                switch (Parm)
                {
                    case "@Id":
                        command.CommandText = Cmdtx_SLCT_byID_Tbl_Contract;
                        break;
                    case "@Contract":
                        command.CommandText = Cmdtx_SLCT_byCntr_Tbl_Contract;
                        break;
                    default:
                        return;
                }


                command.Parameters.AddWithValue(Parm, Val);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    res = reader.ToEqoContract();
                }
            });

            return res;
        }

        public EqoContract? SelectById(long Id)
        {
            EqoContract? rowsRead = SelectByBase("@Id", Id);

            return rowsRead;
        }

        public EqoContract? SelectByContract(long ContractNo)
        {
            EqoContract? rowsRead = SelectByBase("@Contract", ContractNo);

            return rowsRead;
        }
    }
}
