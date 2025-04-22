using EwkQxObd.Data;

namespace EwkQxObd.UnitTest
{
    public class Data_EqoDataStoreTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void DropTableTest()
        {
            using EqoDataStore store = new();

            store.DropTable();

            Assert.IsNotNull(store);
        }

        [Test]
        public void CreateTableTest()
        {
            using EqoDataStore store = new();

            store.CreateTables();

            Assert.IsNotNull(store);
        }

        [TestCase(2233418u)]
        [Test]
        public void InsertContractTest(
            uint ContractNo)
        {
            var from = new DateOnly(1998, 3, 2);
            var to = new DateOnly(2000, 4, 7);
            using EqoDataStore store = new();

            int rowsAffected = store.InsertContracts(ContractNo,
                from, to);

            Assert.That(rowsAffected, Is.EqualTo(1));
        }
    }
}