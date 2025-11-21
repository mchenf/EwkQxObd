using EwkQxObd.Api;
using EwkQxObd.Api.Authentication;
using EwkQxObd.Pwsh.IqxOrganization;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EwkQxObd.UnitTest
{
    public class Api_Tests
    {
        private IConfiguration? config = null;

        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder()
                .AddUserSecrets<Api_Tests>()
                .Build();
        }


        [Test]
        public void GetUserOrganization()
        {
            if (config == null)
            {
                Assert.Fail();
                return;
            }
            string bearer = config["IQX:Bearer"];

            IqxOrganizationGet cmdlet = new();

            Console.WriteLine(bearer);

            cmdlet.Bearer = bearer;

            var res = cmdlet.Invoke();

            var enmu = res.GetEnumerator();
            while (enmu.MoveNext())
            {
                Console.WriteLine(enmu.Current);
            }
        }

        [Test]
        public async Task AuthTest()
        {
            if (config == null)
            {
                Assert.Fail();
                return;
            }
            var client = new Client();

            

            client.AuthUrl = config["API:AuthUrl"];

            client.Request.GrantType = "password";
            client.Request.ClientId = config["API:ClientId"];

            client.Request.ClientSecret = config["API:ClientSecret"];

            client.Request.Audience = @"https://fossanalytics.com/fossdigital";

            client.Request.Scope = @"openid profile address email offline_access FossAPI GlobalPublicKeyInfrastructureService";

            await client.Authenticate();

            Assert.Multiple(() =>
            {
                Assert.That(client.HasResponse, Is.True);
                Assert.That(client.IsNotExpired, Is.True);
                Assert.That(client.IsAuthenticated, Is.True);
            });

            string resp = JsonSerializer.Serialize(client.Response);

            await Console.Out.WriteLineAsync(resp);
        }

    }
}
