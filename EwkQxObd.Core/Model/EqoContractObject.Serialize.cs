using EwkQxObd.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    public partial class EqoContractObject : ITextFlattable
    {

        private const string format1 = 
@"==== Contract ====
Contract Number: {0}
Description: {1}
Customer Contact: {2}
Employee Responsible: {3}
Valid From: {4}
Valid To: {5}

==== Ship-To ====
Account Number: {6}
Account Name: {7}
Region: {8}
Country: {9}

{10}";

        private const string format2 = 
@"==== Instrument NOT On IQX ====
Serial Number: {0}
Instrument Type: {1}";

        public string ToFlatText()
        {
            string emailCustomer = Contract!.CustomerContact is null ?
                "<Not Specified>" :
                string.Format("{0}<{1}>", Contract.CustomerContact.FullName,
                    Contract.CustomerContact.EmailAddress ?? "email@not.exist"
                );

            string emailEmployee = Contract!.EmployeeResponsible is null ?
                "<Not Specified>" :
                string.Format("{0}<{1}>", Contract.EmployeeResponsible.FullName,
                    Contract.EmployeeResponsible.EmailAddress ?? "email@not.exist"
                );

            string syngio = string.Empty;

            if (InstrumentConnected is null)
            {
                syngio = string.Format(format2,
                    SerialNumber,
                    InstrumentType
                );
            }
            else
            {
                syngio = InstrumentConnected.ToFlatText();
            }

            string result = string.Format(format1,
                Contract.ContractNumber,
                Contract.Description,
                emailCustomer,
                emailEmployee,
                Contract.ValidFrom.ToString("MM/dd/yyyy"),
                Contract.ValidTo.ToString("MM/dd/yyyy"),
                ShipTo!.AccountNumber,
                ShipTo!.Name,
                ShipTo!.Region,
                ShipTo!.Country,
                syngio
            );



            return result;
        }
    }
}
