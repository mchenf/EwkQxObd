using EwkQxObd.Data;

namespace EwkQxObd.UnitTest
{
    public class Data_EqoAccountTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ConnectTests()
        {
            var EqoAccountQ = new EqoAccountQuery();

            EqoAccountQ.Connect();
            Assert.Pass();
        }
    }
}