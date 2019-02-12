namespace CachingTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Caching;
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class CacheTests
    {
        private Mock<IDictionary<string, CacheItem>> dictionaryMock;

        [SetUp]
        public void Setup()
        {
            this.dictionaryMock = new Mock<IDictionary<string, CacheItem>>();
        }

        [Test]
        public void AddItem_AddItemWithSameKey_ThrowInvalidOperationException()
        {
            this.dictionaryMock.Setup(x => x.ContainsKey("item1")).Returns(true);
            Cache cache = new Cache(this.dictionaryMock.Object);
            Assert.Throws<InvalidOperationException>(() => cache.AddItem("item1", "item2", 3));
        }

        [Test]
        public void AddItem_AddItemWithNegativeStorageTime_ThrowArgumentOutOfRangeException()
        {
            Cache cache = new Cache(this.dictionaryMock.Object);
            Assert.Throws<ArgumentOutOfRangeException>(() => cache.AddItem("ITEM1", null, -5));
        }

        [Test]
        public void GetItem_GetItemByInvalidKey_ThrowKeyNotFoundException()
        {
            this.dictionaryMock.Setup(x => x.ContainsKey("item5")).Returns(false);
            Cache cache = new Cache(this.dictionaryMock.Object);
            Assert.Throws<KeyNotFoundException>(() => cache.GetItem("item5"));
        }

        [Test]
        public void GetItem_GetExpiredItem_ThrowInvalidOperationException()
        {
            this.dictionaryMock.Setup(x => x["item1"]).Returns(new CacheItem(null, 1));
            this.dictionaryMock.Setup(x => x.ContainsKey("item1")).Returns(true);
            Cache cache = new Cache(this.dictionaryMock.Object);
            Thread.Sleep(1500);
            Assert.Throws<InvalidOperationException>(() => cache.GetItem("item1"));
        }

        [Test]
        public void GetItem_GetValidItem_ReturnItem()
        {
            this.dictionaryMock.Setup(x => x["item1"]).Returns(new CacheItem("content", 1));
            this.dictionaryMock.Setup(x => x.ContainsKey("item1")).Returns(true);
            this.dictionaryMock.Setup(x => x["item2"]).Returns(new CacheItem(3, 1));
            this.dictionaryMock.Setup(x => x.ContainsKey("item2")).Returns(true);
            Cache cache = new Cache(this.dictionaryMock.Object);
            Assert.AreEqual("content", cache.GetItem("item1"));
            Assert.AreEqual(3, cache.GetItem("item2"));
        }
    }
}
