using System;
using System.Collections.Generic;
using System.Linq;
using Crypto;
using Newtonsoft.Json;
using NUnit.Framework;
using Rhino.Mocks;


namespace UnitTest
{
    [TestFixture]
    public class Sha256Tests
    {
        private Sha256 _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Sha256();
        }

        [Test]
        public void CanComputeKnownHash()
        {
            const string toComputeFrom = "SHA-256 hash to be converted";
            const string knownHash = "c8e23368e5dd263cf0c4617930c2c814e3038a4784b7db0967f7c74375536c35";

            var result = _sut.GetHash(toComputeFrom);
            Assert.AreEqual(result, knownHash);
        }

        [Test]
        public void CanComputeHashForObject()
        {
            var testObject = new ParentTestClass
            {
                ParentName = Guid.NewGuid().ToString(),
                ChildrenList = new List<ChildTestClass>
                {
                    new ChildTestClass
                    {
                        ChildName = Guid.NewGuid().ToString()
                    },
                    new ChildTestClass
                    {
                        ChildName = Guid.NewGuid().ToString()
                    },
                }
            };

            var json = JsonConvert.SerializeObject(testObject);
            var knownHash = _sut.GetHash(json);

            var result = _sut.GetHash(testObject);
            Assert.AreEqual(knownHash, result);

        }

        public class ParentTestClass
        {
            public string ParentName { get; set; }
            public List<ChildTestClass> ChildrenList { get; set; }
        }

        public class ChildTestClass
        {
            public string ChildName { get; set; }
        }
    }
}