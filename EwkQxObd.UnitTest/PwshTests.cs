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
    }
}
