using EwkQxObd.Api;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task AuthTest()
        {
            if (config == null)
            {
                Assert.Fail();
                return;
            }
            var client = new ClientAuth();

            

            client.AuthUrl = config["API:AuthUrl"];

            client.Request.GrantType = "password";
            client.Request.ClientId = config["API:ClientId"];

            client.Request.ClientSecret = config["API:ClientSecret"];

            client.Request.Audience = @"https://fossanalytics.com/fossdigital";

            client.Request.Scope = @"openid profile address email offline_access FossAPI GlobalPublicKeyInfrastructureService";

            await client.Authenticate(config["username"], config["password"]);


        }

    }
}
