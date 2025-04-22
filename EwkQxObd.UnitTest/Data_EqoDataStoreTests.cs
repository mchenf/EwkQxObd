using EwkQxObd.Data;
using EwkQxObd.Data.TableContract;

namespace EwkQxObd.UnitTest
{
    public class Data_EqoDataStoreTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test, Order(0)]
        public void DropTableTest()
        {
            using EqoDropTblContract store = new();

            store.DropTable();

            Assert.IsNotNull(store);
        }

        [Test, Order(1)]
        public void CreateTableTest()
        {
            using EqoCreateTblContract store = new();

            store.CreateTables();

            Assert.IsNotNull(store);
        }

        [TestCase(2233418u)]
        [Test, Order(2)]
        public void InsertContractTest(
            uint ContractNo)
        {
            var from = new DateOnly(1998, 3, 2);
            var to = new DateOnly(2000, 4, 7);
            using EqoInsertTblContract store = new();

            int rowsAffected = store.InsertContracts(ContractNo,
                from, to);

            Assert.That(rowsAffected, Is.EqualTo(1));
        }
    }
}