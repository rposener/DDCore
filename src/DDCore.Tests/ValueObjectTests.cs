using DDCore.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DDCore.Tests
{
    [TestClass]
    public class ValueObjectTests
    {
        ValueObject valueObject1;
        ValueObject valueObject2;
        ValueObject valueObject3;
        ValueObject valueObject4;


        class TestValueClass : ValueObject
        {
            int _id;
            string _name;
            public int Id
            {
                get { return _id; }
            }
            public string Name
            {
                get { return _name; }
            }
            public TestValueClass(int id, string name)
            {
                _id = id;
                _name = name;
            }

            protected override IEnumerable<IComparable> GetEqualityComponents()
            {
                yield return Id;
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            valueObject1 = new TestValueClass(100, "OMH");
            valueObject2 = new TestValueClass(200, "XYZ");
            valueObject3 = new TestValueClass(200, "ABC");
            valueObject4 = new TestValueClass(300, "ABC");
        }

        [TestMethod]
        public void CheckEquality()
        {
            Assert.AreNotEqual(valueObject1, valueObject2, "Objects are Equal");
            Assert.AreEqual(valueObject2, valueObject3, "Objects are not Equal");
            Assert.AreNotEqual(valueObject3, valueObject4, "Objects are Equal");

        }

        [TestMethod]
        public void CheckToString()
        {
            // Test
            var strResult = valueObject1.ToString();
            // Assert
            Assert.AreEqual("TestValueClass:100", strResult, "tostring not as expected");
        }

        [TestMethod]
        public void CheckSortability()
        {
            // Setup
            var list = new List<ValueObject>(new[] { valueObject3, valueObject1, valueObject4, valueObject2 });
            // Test
            list.Sort();
            // Assert
            Assert.AreEqual(list[0], valueObject1, "first item not sorted");
            Assert.AreEqual(list[1], valueObject3, "second item not sorted");
            Assert.AreEqual(list[2], valueObject2, "third item not sorted");
            Assert.AreEqual(list[3], valueObject4, "fourth item not sorted");
        }
    }
}
