using EwkQxObd.Core.Model;
using EwkQxObd.Data.TableContract;
using EwkQxObd.Pwsh.Contract;
using EwkQxObd.Pwsh.Installation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.UnitTest
{
    class PwshTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test, Order(0)]
        public void Install_EwkIqxObd_Test()
        {
            var cmd = new EwkIqxObd_Install();
            var res = cmd.Invoke();

            foreach (var item in res)
            {
                Console.WriteLine(item);

            }
        }

        [Test, Order(1)]
        public void Get_Contract_Test()
        {
            var cmd = new ContractGet();
            cmd.ContractNumber = "xxxxxx";
            var res = cmd.Invoke();
            foreach (var item in res)
            {
                Console.WriteLine(item);

            }
        }

        [Test, Order(2)]
        [TestCase(167321)]
        [TestCase(-33)]
        public void Get_Account_Test(long PartnerID)
        {
            var cmd = new AccountGet();
            cmd.PartnerID = PartnerID; //167321;
            var res = cmd.Invoke();


            int count = 0;

            EqoAccount? account = null;
            foreach (var item in res)
            {
                Console.WriteLine(item);
                account = item as EqoAccount;
                count++;
            }

            Assert.That(count, Is.EqualTo(1), "This cmdlet should give out only one result");
            Assert.That(account, Is.Not.Null, "This cmdlet should not give out null object result");
            Assert.That(account, Is.TypeOf<EqoAccount>(), $"This cmdlet should have result of {nameof(EqoAccount)}");

            long Expected = PartnerID;
            if (PartnerID < 0)
            {
                Expected = long.MinValue;
            }
            Assert.That(account.PartnerId, Is.EqualTo(Expected), $"This cmdlet should only get account number same as input");

            
        }

        [Test, Order(3)]
        public void New_Account_Test()
        {
            var cmd = new AccountNew();


            cmd.PartnerID = 167321;

            cmd.PartnerName = "Test Partner Again";
            cmd.GeisGuid = Guid.NewGuid();

            cmd.Country = "Fellahahaz";
            cmd.Region = "Creazzg";


            var res = cmd.Invoke();
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}
