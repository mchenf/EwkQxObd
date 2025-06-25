using EwkQxObd.Api.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EwkQxObd.Pwsh.IqxOrganization
{
    [Cmdlet(VerbsCommon.Get, "IqxOrganization")]
    public class IqxOrganizationGet : Cmdlet
    {
        private static HttpClient? _httpClient;
        private const string requestUrl = @"https://app.iqx.net/iqx/common-bff-api/v1/users/getUserOrganisations";

        [Parameter(Mandatory = true, Position = 0)]
        public string? Bearer { get; set; }

        public IqxOrganizationGet()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();

            }
        }

        protected override void BeginProcessing()
        {

            WriteInformation(new InformationRecord
            (
                "Beginning Process",
                nameof(BeginProcessing)
            ));
        }

        protected override void EndProcessing()
        {
            WriteInformation(new InformationRecord
            (
                "Ending Process",
                nameof(EndProcessing)
            ));
        }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(Bearer))
            {
                WriteWarning("No bearer token provided");
                return;
            }
            if (_httpClient == null)
            {
                WriteWarning($"{nameof(_httpClient)} is null");
                return;
            }
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Add("Authorization", Bearer);

            using var response = _httpClient.Send(request);

            response.EnsureSuccessStatusCode();

            var results = 
                JsonSerializer.Deserialize<List<IqxOrgResponse>>(response.Content.ReadAsStream());

            if (results != null)
            {
                WriteObject(results);
            }
            else
            {
                WriteInformation(new InformationRecord
            (
                "No results found",
                nameof(EndProcessing)
            ));
            }

        }

        protected override void StopProcessing()
        {
            throw new NotImplementedException();
        }
    }
}
