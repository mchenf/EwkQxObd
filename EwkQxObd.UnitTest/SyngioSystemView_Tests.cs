using EwkQxObd.Core.Model.Views;
using EwkQxObd.Core.Serialization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.UnitTest
{
    public class SyngioSystemView_Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }


        [Test]
        [TestCase("Achin", 33, 26, 177, 123)]
        public void SyngioSystemView_Test(string system, int ntwk, int ntwklinked, int inst, int instlinked)
        {
            var obj = new SyngioViewSystem();
            obj.System = system;
            obj.Networks = ntwk;
            obj.Network_Linked = ntwklinked;
            obj.Instruments = inst;
            obj.Instrument_Linked = instlinked;

            Console.WriteLine(obj.Progress_Instrument);
            Console.WriteLine(obj.Progress_Network);

            Assert.IsTrue(true);



        }
    }
}
