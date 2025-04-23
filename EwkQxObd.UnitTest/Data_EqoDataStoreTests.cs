using EwkQxObd.Core.Model;
using EwkQxObd.Data;
using EwkQxObd.Data.TableContract;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

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


        private Dictionary<long, long> _testIDs = new();

        [Test, Order(4)]
        public void SelectById()
        {
            //Total ID is 108 for each test, minimal
            
            var rand = RandomNumberGenerator.Create();

            int selectLen = 8;
            byte[] randBytes = new byte[selectLen];
            rand.GetNonZeroBytes(randBytes);
            for (int i = 0; i < selectLen; i++)
            {
                _testIDs.TryAdd(randBytes[i], -1);
            }
            using EqoSelectWhereTblContract store = new();


            foreach (var item in _testIDs)
            {
                EqoContract? res = store.SelectById(item.Key);
                Console.WriteLine("Trying To Select {0}", item.Key);

                Assert.That(res, Is.Not.Null);
                Assert.That(res.Id, Is.EqualTo(item.Key));

                res.Cout();
                _testIDs[item.Key] = res.ContractNumber;
            }
        }

        [Test, Order(5)]
        public void SelectByContract()
        {

            Assert.That(_testIDs.Count(), Is.GreaterThan(0));
            using EqoSelectWhereTblContract store = new();


            foreach (var item in _testIDs)
            {
                EqoContract? res = store.SelectByContract(item.Value);


                Assert.That(res, Is.Not.Null);
                Assert.That(res.Id, Is.EqualTo(item.Key));

                res.Cout();
            }
        }
    }
}