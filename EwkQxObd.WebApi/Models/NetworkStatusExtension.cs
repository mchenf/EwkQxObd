using EwkQxObd.Core.Model.Views;
using System.Text;

namespace EwkQxObd.WebApi.Models
{
    public static class NetworkStatusExtension
    {
        private const string seperator1 = "==================";
        private const string seperator2 = "------------------";
        private const string fmtNetwork = "{0} \\ [{1}] {2}";

        private const string fmtOrganiz =
            "<{0}> {1}\r\n  Street: {2}\r\n    City: {3}\r\n Country: {4}";


        private const string fmtInstLine = "{0,-14} {1, -14} {2}";


        public static string ShareInfo(this List<InstrumentLinkStatus> Instruments)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("IQX Onboarding Status");
            sb.AppendLine("Network View");
            sb.AppendLine(seperator1);

            InstrumentLinkStatus networkInfo = Instruments[0];

            //1st diverge: What if there is no record of this in instruments?

            if (networkInfo.System == "disconnected")
            {
                sb.AppendLine("[WARNING] INSTRUMENT OFFLINE");
            }
            else
            {
                sb.Append("Queried at: ");
                sb.AppendLine(networkInfo.QueryTimeStamp.ToString());
                sb.AppendLine(seperator2);
                sb.AppendFormat(fmtNetwork,
                    networkInfo.System,
                    networkInfo.NetworkId,
                    networkInfo.NetworkName);
                sb.AppendLine();
            }

            sb.AppendLine(seperator2);

            //2nd diverge: connected to is null

            if (networkInfo.ConnectedTo is null)
            {
                sb.AppendLine("[WARNING] NETWORK NOT CONNECTED To IQX");
            }
            else
            {
                sb.AppendLine("Connected Organization:");
                sb.AppendFormat(fmtOrganiz,
                    networkInfo.ConnectedTo.AccountNumber,
                    networkInfo.ConnectedTo.Name,
                    networkInfo.ConnectedTo.Street,
                    networkInfo.ConnectedTo.City,
                    networkInfo.ConnectedTo.Country
                );
                sb.AppendLine();
            }


            sb.AppendLine(seperator2);

            sb.AppendLine("Instruments:");
            sb.AppendFormat(fmtInstLine,
                "Serial No.",
                "Contract No.",
                "Instr. Group"
            );
            sb.AppendLine();
            foreach (InstrumentLinkStatus i in Instruments)
            {
                sb.AppendFormat(fmtInstLine,
                string.IsNullOrEmpty(i.SerialNumber) ? "--------" : i.SerialNumber,
                string.IsNullOrEmpty(i.ContractNumber.ToString()) ? "--------" : i.ContractNumber,

                i.InstrumentGroup
                );
                sb.AppendLine();
            }

            sb.AppendLine("EOF");

            return sb.ToString();
        }

    }
}
