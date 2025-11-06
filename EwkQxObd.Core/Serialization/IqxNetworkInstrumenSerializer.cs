using EwkQxObd.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Serialization
{
    [Obsolete]
    public static class IqxNetworkInstrumenSerializer
    {
        private const string ignore = "\"System\",\"Query Timestamp\",\"Network Name\",\"NetworkID\",\"GEIS GUID\",\"Instrument Group\",\"Instrument SN\",\"Instrument Name\"";
        private const string NULL = "NULL";
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

            result.System = fields[0];
            result.QueryTimeStamp = DateTime.Parse(fields[1]);

            result.NetworkName = fields[2] == NULL ? null : fields[2];

            if (long.TryParse(fields[3], out long f3Result))
            {
                result.NetworkId = f3Result;
            }
            else 
            {
                result.NetworkId = null;
            }

            if (!Guid.TryParse(fields[4], out Guid f4Result))
            {
                f4Result = default;
            }

            result.LinkedAccountGuid = f4Result;

            result.InstrumentGroup = fields[5] == NULL ? null : fields[5];


            result.SerialNumber = fields[6];
            result.InstrumentName = fields[7];





            return result;
        }
    }
}
