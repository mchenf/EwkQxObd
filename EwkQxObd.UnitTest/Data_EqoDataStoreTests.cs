using EwkQxObd.Data;
using EwkQxObd.Data.TableContract;
using System.Diagnostics.Contracts;

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
        public void RandInsertContractTest()
        {
            var RandTestCase = new TestInsertContractFixture();
            using EqoInsertTblContract store = new();

            foreach ((long, DateOnly, DateOnly) item in RandTestCase)
            {

                int rowsAffected = 
                    store.InsertContracts(
                        item.Item1,
                        item.Item2,
                        item.Item3
                    );
                Assert.That(rowsAffected, Is.EqualTo(1));
            }
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