using System;
using System.Collections.Generic;
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
        #region Test Classes

        /// <summary>
        /// A test filter class.
        /// </summary>
        /// <seealso cref="IFilter" />
        public class TestFilter : IFilter
        {
            /// <summary>
            /// 13927ebd-c8bb-4e75-87d7-1fea1a96d220
            /// </summary>
            public Guid Id => Guid.Parse("13927ebd-c8bb-4e75-87d7-1fea1a96d220");

            /// <summary>
            /// Test Filter
            /// </summary>
            public string Label => "Test Filter";

            public List<T> Apply<T>(List<T> items, FilterComparison comparison, string compareAgainst)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// A test order class.
        /// </summary>
        /// <seealso cref="IOrdering" />
        public class TestOrder : IOrdering
        {
            /// <summary>
            /// 267fde69-97b1-43c7-a9ec-f488f7554dd5
            /// </summary>
            public Guid Id => Guid.Parse("267fde69-97b1-43c7-a9ec-f488f7554dd5");

            /// <summary>
            /// Test Order
            /// </summary>
            public string Label => "Test Order";

            public List<T> Apply<T>(List<T> items, OrderingDirection direction)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

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
        /// Tests the filterset to string method.
        /// </summary>
        [Test]
        public void FiltersetToStringTest()
        {
            var filterset1 = Filterset.Create();
            var filterset2 = Filterset.Create("Filterset");

            Assert.AreEqual(filterset1.ToString(), filterset1.Id.ToString());
            Assert.AreEqual(filterset2.ToString(), "Filterset");
        }

        /// <summary>
        /// Tests the filterset element create method.
        /// </summary>
        [Test]
        public void CreateFiltersetElementTest()
        {
            var filter = new TestFilter();
            var filterset = Filterset.Create();
            var filtersetElement1 = FiltersetExpressionElement.Create(filterset, FiltersetExpressionElementType.And, 2);
            var filtersetElement2 = FiltersetExpressionElement.Create(filterset, filter, 3);

            Assert.AreEqual(filtersetElement1.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetElement1.ElementType, FiltersetExpressionElementType.And);
            Assert.AreEqual(filtersetElement1.ElementNumber, 2);

            Assert.AreEqual(filtersetElement2.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetElement2.FilterId, filter.Id);
            Assert.AreEqual(filtersetElement2.ElementType, FiltersetExpressionElementType.Filter);
            Assert.AreEqual(filtersetElement2.ElementNumber, 3);
        }

        /// <summary>
        /// Tests the filterset element properties.
        /// </summary>
        [Test]
        public void FiltersetElementPropertiesTest()
        {
            var filter = new TestFilter();
            var filterset = Filterset.Create();
            var filtersetElement1 = new FiltersetExpressionElement
            {
                FiltersetId = filterset.Id,
                ElementNumber = 1,
                ElementType = FiltersetExpressionElementType.OpenParenthesis
            };
            var filtersetElement2 = new FiltersetExpressionElement
            {
                FiltersetId = filterset.Id,
                ElementNumber = 2,
                ElementType = FiltersetExpressionElementType.Filter,
                Filter = filter,
                Comparison = FilterComparison.BeginsWith,
                FilterAgainst = "Something"
            };

            Assert.AreEqual(filtersetElement1.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetElement1.ElementType, FiltersetExpressionElementType.OpenParenthesis);
            Assert.AreEqual(filtersetElement2.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetElement2.FilterId, filter.Id);
            Assert.AreEqual(filtersetElement2.ElementType, FiltersetExpressionElementType.Filter);
            Assert.AreEqual(filtersetElement2.ElementNumber, 2);
            Assert.AreEqual(filtersetElement2.Comparison, FilterComparison.BeginsWith);
            Assert.AreEqual(filtersetElement2.FilterAgainst, "Something");
        }

        /// <summary>
        /// Tests the filterset element to string method.
        /// </summary>
        [Test]
        public void FiltersetElementToStringTest()
        {
            var filter = new TestFilter();
            var filterset = Filterset.Create();
            var filtersetElement1 = FiltersetExpressionElement.Create(filterset, FiltersetExpressionElementType.OpenParenthesis, 1);
            var filtersetElement2 = FiltersetExpressionElement.Create(filterset, filter, 2);
            filtersetElement2.Comparison = FilterComparison.Is;
            filtersetElement2.FilterAgainst = "Something";

            Assert.AreEqual("(", filtersetElement1.ToString());
            Assert.AreEqual("Test Filter Is Something", filtersetElement2.ToString());
        }

        /// <summary>
        /// Tests the filterset order create method.
        /// </summary>
        [Test]
        public void CreateFiltersetOrderTest()
        {
            var order = new TestOrder();
            var filterset = Filterset.Create();
            var filtersetOrder1 = FiltersetOrderingElement.Create(filterset, order, 2);
            var filtersetOrder2 = FiltersetOrderingElement.Create(filterset, order, 3, OrderingDirection.Descending);

            Assert.AreEqual(filtersetOrder1.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetOrder1.OrderId, order.Id);
            Assert.AreEqual(filtersetOrder1.OrderNumber, 2);

            Assert.AreEqual(filtersetOrder2.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetOrder2.OrderId, order.Id);
            Assert.AreEqual(filtersetOrder2.OrderNumber, 3);
            Assert.AreEqual(filtersetOrder2.Direction, OrderingDirection.Descending);
        }

        /// <summary>
        /// Tests the filterset order properties.
        /// </summary>
        [Test]
        public void FiltersetOrderPropertiesTest()
        {
            var order = new TestOrder();
            var filterset = Filterset.Create();
            var filtersetOrder = new FiltersetOrderingElement
            {
                FiltersetId = filterset.Id,
                OrderNumber = 1,
                Order = order,
                Direction = OrderingDirection.Descending
            };

            Assert.AreEqual(filtersetOrder.FiltersetId, filterset.Id);
            Assert.AreEqual(filtersetOrder.OrderNumber, 1);
            Assert.AreEqual(filtersetOrder.Direction, OrderingDirection.Descending);
            Assert.AreEqual(filtersetOrder.OrderId, order.Id);
            Assert.NotNull(filtersetOrder.Order);
        }

        /// <summary>
        /// Tests the filterset order to string method.
        /// </summary>
        [Test]
        public void FiltersetOrderToStringTest()
        {
            var order = new TestOrder();
            var filterset = Filterset.Create();
            var filtersetOrder = FiltersetOrderingElement.Create(filterset, order, 1);

            Assert.AreEqual("Test Order Ascending", filtersetOrder.ToString());
        }
    }
}