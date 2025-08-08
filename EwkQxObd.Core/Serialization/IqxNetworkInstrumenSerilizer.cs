using EwkQxObd.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Serialization
{
    public static class IqxNetworkInstrumenSerilizer
    {
        private const string ignore = "System,Query Timestamp,Network Name,NetworkID,GEIS GUID,Instrument Group,Instrument S/N,Instrument Name";
        public static IqxNetworkInstrument? Deserialize(this string Line, char seperator = ',')
        {
            if (Line == ignore) return null;

            var result = new IqxNetworkInstrument();

            //TODO: Fix the text qualifier containing comma issue, i.e. "Grain, oil, and palm tree" columns
            string[] fields = Line.Split(seperator);

            result.System = fields[0];
            result.QueryTimeStamp = DateTime.Parse(fields[1]);
            result.NetworkName = fields[2];
            result.NetworkId = long.Parse(fields[3]);
            result.LinkedAccountGuid = Guid.Parse(fields[4]);
            result.InstrumentGroup = fields[5];
            result.SerialNumber = fields[6];
            result.InstrumentName = fields[7];





            return result;
        }
    }
}
