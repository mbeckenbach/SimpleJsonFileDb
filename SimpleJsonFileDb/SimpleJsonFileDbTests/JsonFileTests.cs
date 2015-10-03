using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJsonFileDb;
using SimpleJsonFileDbTests.TestModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonFileDbContext.Tests
{
    [TestClass]
    public class JsonFileTests
    {
        public static string _testDataFileDir = "../../TempData";

        [TestMethod]
        public void ShouldWriteToDataFile()
        {
            var file = new JsonFile<TestModel>(_testDataFileDir);
            file.Add(new TestModel { MyTest = "Foo" });

            Assert.AreEqual(1, file.Count);
            Assert.IsInstanceOfType(file, typeof(IList<TestModel>));
        }

        [TestMethod]
        public void ShouldRemoveFromDataFile()
        {
            var file = new JsonFile<TestModel>(_testDataFileDir);
            file.Add(new TestModel { MyTest = "Bar" });

            Assert.AreEqual(2, file.Count);

            var someData = file.First(x => x.MyTest == "Bar");
            file.Remove(someData);

            Assert.AreEqual(1, file.Count);
            Assert.IsInstanceOfType(file, typeof(IList<TestModel>));
        }

        [TestMethod]
        public void ShouldInitWithExistingData()
        {
            var db = new TestDbContext();
            Assert.AreEqual(1, db.SomeTestData.Count);
        }

        [TestMethod]
        public void ShouldInitWithoutExistingData()
        {
            var db = new TestDbContext();
            Assert.AreEqual(0, db.MoreTestData.Count);
        }

        [TestMethod]
        public void ShouldReturnData()
        {
            var db = new TestDbContext();
            var data = db.SomeTestData.FirstOrDefault(x => x.MyTest == "Foo");
            Assert.AreNotEqual(null, data);
        }

        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            DeleteTestDirectory();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            DeleteTestDirectory();
        }

        static void DeleteTestDirectory()
        {
            if (Directory.Exists(_testDataFileDir))
                Directory.Delete(_testDataFileDir, true);
        }
    }
}