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
        private const string ignore = "System,Query Timestamp,Network Name,NetworkID,GEIS GUID,Instrument Group,Instrument SN,Instrument Name";
        public static IqxNetworkInstrument? Deserialize(this string Line, char seperator = ',')
        {
            if (Line == ignore) return null;

            var result = new IqxNetworkInstrument();

            int strLen = Line.Length;

            bool shouldParseSep = true;
            bool needToTrimQuote = false;

            int syncStart = 0;
            int syncEnd = 0;
            string[] fields = new string[8];

            int synPos = 0;

            for (int i = 0; i < strLen; i++)
            {
                if (Line[i] == '"')
                {
                    shouldParseSep = !shouldParseSep;
                    if (shouldParseSep)
                    {
                        needToTrimQuote = true;
                    }
                }


                if (i == strLen - 1 || shouldParseSep && Line[i] == ',')
                {
                    syncEnd = i;

                    int syncLen = i < strLen - 1 ? syncEnd - syncStart : syncEnd - syncStart + 1;

                    fields[synPos] = needToTrimQuote ?
                        Line.Substring(syncStart, syncLen).Replace("\"", string.Empty) :
                        Line.Substring(syncStart, syncLen);

                    needToTrimQuote = false;

                    synPos++;
                    syncStart = syncEnd + 1;



                }
            }


            //TODO: Fix the text qualifier containing comma issue, i.e. "Grain, oil, and palm tree" columns



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
