using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Views;
using System.Text;

namespace EwkQxObd.WebApi.Models.Serialization
{
    public static class InstrStatusExtension
    {
        private const string format1 =
@"IQX Onboarding Status
Instrument View
==================
Queried at: {0}
------------------
Serial Number: {1}
Instrument Name: {6}
{5}
{2}
==== {3} ====
{4}
{7}";
        private const string fmrtIqxPath1 =
@"{0} \ ???";
        private const string fmrtIqxPath2 =
@"{0} \ [{1}] {2}";

        private const string fmrtOrgInfo =
@"{0,15} :     {1}
 Account Number :     {2}
   Account Name :     {3}
         Street :     {4}
           City :     {5}
        Country :     {6}
         Region :     {7}";

        private const string fmrtContract =
@"==== {0} ====
Contract Number : {1}
    Description : {2}
       Valid To : {3}
{4}

EOF
";

        //private const string GuidMask = "****-****-****-****-********";
        private const string GuidMask = "████-████-████-████-████████";

        public static string ObfuscateGeis(this IqxOrganization Org)
        {

            if (Org is null || Org.GeisGuid is null)
            {
                return Guid.Empty.ToString();
            }

            Guid g = Org.GeisGuid ?? Guid.Empty;

            string obfuscatedGuid = g.ToString();

            string obfuscatedGuid_head = obfuscatedGuid[..4];
            string obfuscatedGuid_tail = obfuscatedGuid[^4..];

            obfuscatedGuid = obfuscatedGuid_head + GuidMask + obfuscatedGuid_tail;

            return obfuscatedGuid;
        }

        public static string ShareInfo(this InstrumentLinkStatus linkStatus)
        {
            string IqxConnectedPath = linkStatus.NetworkId is null ? (
                string.IsNullOrEmpty(linkStatus.System) ?
                    "[x] OFFLINE" :
                    string.Format(fmrtIqxPath1, linkStatus.System)
                ) : string.Format(fmrtIqxPath2, linkStatus.System, linkStatus.NetworkId, linkStatus.NetworkName);

            string ConnectedStatus = linkStatus.ConnectedTo is null ?
                "[x] IQX DISCONNECTED" : 
                "IQX CONNECTED";

            string LinkedTo = linkStatus.ConnectedTo is null ?
                "[x] No Linked-to" :
                string.Format(fmrtOrgInfo,
                    "Linked To",
                    linkStatus.ConnectedTo.ObfuscateGeis(),
                    linkStatus.ConnectedTo.AccountNumber,
                    linkStatus.ConnectedTo.Name,
                    linkStatus.ConnectedTo.Street,
                    linkStatus.ConnectedTo.City,
                    linkStatus.ConnectedTo.Country,
                    linkStatus.ConnectedTo.Region
                );

            string ShipTo = linkStatus.ShippedTo is null ?
                string.Empty :
                string.Format(fmrtOrgInfo,
                    "Ship To",
                    linkStatus.ShippedTo.ObfuscateGeis(),
                    linkStatus.ShippedTo.AccountNumber,
                    linkStatus.ShippedTo.Name,
                    linkStatus.ShippedTo.Street,
                    linkStatus.ShippedTo.City,
                    linkStatus.ShippedTo.Country,
                    linkStatus.ShippedTo.Region
                );

            string ContractInfo = linkStatus.ContractNumber is null ?
                "[x] Missing Contract" :
                string.Format(fmrtContract,
                    "IQX Contract",
                    linkStatus.ContractNumber,
                    linkStatus.Description,
                    linkStatus.ValidTo,
                    ShipTo
                );

            return string.Format(format1,
                linkStatus.QueryTimeStamp,
                linkStatus.SerialNumber,
                IqxConnectedPath,
                ConnectedStatus,
                LinkedTo,
                linkStatus.InstrumentType,
                linkStatus.InstrumentName,
                ContractInfo
            );

        }
    }
}
