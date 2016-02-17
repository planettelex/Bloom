using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the filterset model classes.
    /// </summary>
    [TestFixture]
    public class FiltersetModelTests
    {
        public class TestFilter : IFilter
        {
            public Guid Id { get { return Guid.Parse("13927ebd-c8bb-4e75-87d7-1fea1a96d220"); } }

            public string Name { get { return "TestFilter"; } }

            public string Label { get { return "Test Filter"; } }
        }

        public class TestOrder : IOrder
        {
            public Guid Id { get { return Guid.Parse("267fde69-97b1-43c7-a9ec-f488f7554dd5"); } }

            public string Name { get { return "TestOrder"; } }

            public string Label { get { return "Test Order"; } }
        }

        /// <summary>
        /// Tests the filterset create method.
        /// </summary>
        [Test]
        public void CreateFiltersetTest()
        {
            var filterset1 = Filterset.Create();
            var filterset2 = Filterset.Create("Filterset");

            Assert.AreNotEqual(filterset1.Id, Guid.Empty);
            Assert.AreNotEqual(filterset2.Id, Guid.Empty);
            Assert.AreEqual(filterset2.Name, "Filterset");
        }

        /// <summary>
        /// Tests the filterset properties.
        /// </summary>
        [Test]
        public void FiltersetPropertiesTest()
        {
            var id = Guid.NewGuid();
            var filterset = new Filterset
            {
                Id = id,
                Name = "Filterset"
            };

            Assert.AreNotEqual(filterset.Id, Guid.Empty);
            Assert.AreEqual(filterset.Name, "Filterset");
        }

        /// <summary>
        /// Tests the filterset element create method.
        /// </summary>
        [Test]
        public void CreateFiltersetElementTest()
        {
            var filter = new TestFilter();
            var filterset = Filterset.Create();
            var filtersetElement1 = FiltersetElement.Create(filterset, FiltersetElementType.And, filter, 2);
            var filtersetElement2 = FiltersetElement.Create(filterset, filter, 3);

            Assert.AreEqual(filtersetElement1.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetElement1.FilterId, filter.Id);
            Assert.AreEqual(filtersetElement1.ElementType, FiltersetElementType.And);
            Assert.AreEqual(filtersetElement1.ElementNumber, 2);

            Assert.AreEqual(filtersetElement2.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetElement2.FilterId, filter.Id);
            Assert.AreEqual(filtersetElement2.ElementType, FiltersetElementType.Statement);
            Assert.AreEqual(filtersetElement2.ElementNumber, 3);
        }

        /// <summary>
        /// Tests the filterset order create method.
        /// </summary>
        [Test]
        public void CreateFiltersetOrderTest()
        {
            var order = new TestOrder();
            var filterset = Filterset.Create();
            var filtersetOrder1 = FiltersetOrder.Create(filterset, order, 2);
            var filtersetOrder2 = FiltersetOrder.Create(filterset, order, 3, OrderDirection.Descending);

            Assert.AreEqual(filtersetOrder1.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetOrder1.OrderId, order.Id);
            Assert.AreEqual(filtersetOrder1.Priority, 2);

            Assert.AreEqual(filtersetOrder2.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetOrder2.OrderId, order.Id);
            Assert.AreEqual(filtersetOrder2.Priority, 3);
            Assert.AreEqual(filtersetOrder2.Direction, OrderDirection.Descending);
        }
    }
}