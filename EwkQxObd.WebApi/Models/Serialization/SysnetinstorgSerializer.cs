using EwkQxObd.Core.Model.Views;

namespace EwkQxObd.WebApi.Models.Serialization
{
    [Obsolete]
    public static class SysnetinstorgSerializer
    {
        private const string format1 = @"
==== Instrument Info ====
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
City: {9}
";


        public static string ToFlatText(this Syngio obj)
        {
            
            string result = string.Format(format1,
                obj.System,
                obj.NetworkId,
                obj.NetworkName,
                obj.InstrumentGroup,
                obj.SerialNumber,

                obj.LinkedAccountGuid,
                obj.AccountNumber,
                obj.Name,
                obj.Street,
                obj.City
            );



            return result;
        }
    }
}
