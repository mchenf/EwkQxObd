using EwkQxObd.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public partial class Syngio : ITextFlattable
    {
        private const string format1 = 
@"==== Instrument Info ====
System: {0}
Network Id: {1}
Network Name: {2}
Inst. Group: {3}
Serial Number: {4}

==== Linked on IQX ====
Linked To: {5}
Account Number: {6}
Account Name: {7}
Street: {8}
City: {9}";
        private const string GuidMask = "****-****-****-****-********";

        public string ToFlatText()
        {
            string obfuscatedGuid = string.Empty;
            if (LinkedAccountGuid == Guid.Empty)
            {
                obfuscatedGuid =  "** IQX NOT LINKED **";
            }
            else
            {
                obfuscatedGuid = LinkedAccountGuid.ToString();

                string obfuscatedGuid_head = obfuscatedGuid[..4];
                string obfuscatedGuid_tail = obfuscatedGuid[^4..];

                obfuscatedGuid = obfuscatedGuid_head + GuidMask + obfuscatedGuid_tail;
            }



            string result = string.Format(format1,
                System,
                NetworkId,
                NetworkName,
                InstrumentGroup,
                SerialNumber,
                obfuscatedGuid,
                AccountNumber,
                Name,
                Street,
                City
            );
            return result;
        }
    }
}
