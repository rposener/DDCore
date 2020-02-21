using DDCore.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDCore.Tests
{
    [TestClass]
    public class EntityObjectTests
    {
        EntityObject entityObject1;
        EntityObject entityObject2;
        EntityObject entityObject3;
        EntityObject entityObject4;

        class TestEntityClass : EntityObject
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
            public TestEntityClass(int id, string name)
            {
                _id = id;
                _name = name;
            }

            protected override IEnumerable<IComparable> GetIdentityComponents()
            {
                yield return Id;
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            entityObject1 = new TestEntityClass(100, "OMH");
            entityObject2 = new TestEntityClass(200, "XYZ");
            entityObject3 = new TestEntityClass(200, "ABC");
            entityObject4 = new TestEntityClass(300, "ABC");
        }


        [TestMethod]
        public void CheckToString()
        {
            // Test
            var strResult = entityObject1.ToString();
            // Assert
            Assert.AreEqual("TestEntityClass:100", strResult, "tostring not as expected");
        }

        [TestMethod]
        public void CheckEquality()
        {
            Assert.AreNotEqual(entityObject1, entityObject2, "Objects are Equal");
            Assert.AreEqual(entityObject2, entityObject3, "Objects are not Equal");
            Assert.AreNotEqual(entityObject3, entityObject4, "Objects are Equal");

        }
    }
}
