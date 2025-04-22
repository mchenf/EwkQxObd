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

        [Test, Order(2)]
        [TestCaseSource(typeof(TestInsertContractFixture))]
        public void RandInsertContractTest(
            long ContractNo,
            DateOnly from,
            DateOnly to)
        {
            
            using EqoInsertTblContract store = new();

            int rowsAffected = store.InsertContracts(ContractNo,
                from, to);

            Assert.That(rowsAffected, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void SelectAllContractTest()
        {
            
            using EqoSelectTblContract store = new();

            store.SelectAll();

            Assert.That(store, Is.Not.Null);
        }
    }
}