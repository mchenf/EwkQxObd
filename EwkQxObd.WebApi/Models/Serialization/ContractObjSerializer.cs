using EwkQxObd.Core.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace EwkQxObd.WebApi.Models.Serialization
{
    [Obsolete]
    public static class ContractObjSerializer
    {
        private const string format1 = @"
==== Contract ====
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

==== Instrument ====
Serial Number: {10}
Instrument Type: {11}
";


        public static string ToFlatText(this EqoContractObject obj)
        {
            string emailCustomer = obj.Contract!.CustomerContact is null ?
                "<Not Specified>" :
                string.Format("{0}<{1}>", obj.Contract.CustomerContact.FullName,
                    obj.Contract.CustomerContact.EmailAddress ?? "email@not.exist"
                );

            string emailEmployee = obj.Contract!.EmployeeResponsible is null ?
                "<Not Specified>" :
                string.Format("{0}<{1}>", obj.Contract.EmployeeResponsible.FullName,
                    obj.Contract.EmployeeResponsible.EmailAddress ?? "email@not.exist"
                );

            string result = string.Format(format1,
                obj.Contract.ContractNumber,
                obj.Contract.Description,
                emailCustomer,
                emailEmployee,
                obj.Contract.ValidFrom.ToString("MM/dd/yyyy"),
                obj.Contract.ValidTo.ToString("MM/dd/yyyy"),
                obj.ShipTo!.PartnerId,
                obj.ShipTo!.PartnerName,
                obj.ShipTo!.Region,
                obj.ShipTo!.Country,
                obj.SerialNumber,
                obj.InstrumentType
            );



            return result;
        }
    }
}
