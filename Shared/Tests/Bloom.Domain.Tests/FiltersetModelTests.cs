using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using Bloom.Domain.Models.Filters;
using Bloom.Domain.Models.Orders;
using NUnit.Framework;

namespace Bloom.Domain.Tests
{
    /// <summary>
    /// Tests for the filterset model class.
    /// </summary>
    [TestFixture]
    public class FiltersetModelTests
    {
        /// <summary>
        /// Tests the filterset create method.
        /// </summary>
        [Test]
        public void CreateFiltersetTest()
        {
            var filterset = Filterset.Create();

            Assert.AreNotEqual(filterset.Id, Guid.Empty);
        }

        /// <summary>
        /// Tests adding an element to a filterset.
        /// </summary>
        [Test]
        public void AddElementToFiltersetTest()
        {
            var filterset = Filterset.Create();
            var element1 = filterset.AddElement(FiltersetElementType.OpenParenthesis, 1);
            var element2 = filterset.AddElement(FiltersetElementType.CloseParenthesis, 2);

            Assert.AreEqual(filterset.Elements.Count, 2);
            Assert.AreEqual(element1.ElementType, FiltersetElementType.OpenParenthesis);
            Assert.AreEqual(element1.ElementNumber, 1);
            Assert.AreEqual(element2.ElementType, FiltersetElementType.CloseParenthesis);
            Assert.AreEqual(element2.ElementNumber, 2);
        }

        /// <summary>
        /// Tests adding a statement element to a filterset.
        /// </summary>
        [Test]
        public void AddStatementElementToFiltersetTest()
        {
            var filterset = Filterset.Create();
            var filter = new AlbumNameFilter();
            var statement = FiltersetStatement.Create(filter, FilterComparison.Contains, FiltersetItemScope.Album);
            var element = filterset.AddElement(statement, 1);

            Assert.AreEqual(filterset.Elements.Count, 1);
            Assert.AreEqual(element.ElementType, FiltersetElementType.Statement);
            Assert.AreEqual(element.StatementId, statement.Id);
            Assert.AreEqual(element.Statement.FilterId, filter.Id);
            Assert.AreEqual(element.Statement.Comparison, FilterComparison.Contains);
            Assert.AreEqual(element.Statement.Scope, FiltersetItemScope.Album);
        }

        /// <summary>
        /// Tests adding an ordering to a filterset.
        /// </summary>
        [Test]
        public void AddOrderingToFiltersetTest()
        {
            var filterset = Filterset.Create();
            var order = new AlbumNameOrder();
            var ordering = filterset.AddOrdering(order, 1, FiltersetItemScope.Album);

            Assert.AreEqual(filterset.Ordering.Count, 1);
            Assert.AreEqual(ordering.OrderId, order.Id);
            Assert.AreEqual(ordering.Priority, 1);
            Assert.AreEqual(ordering.Scope, FiltersetItemScope.Album);
        }
    }
}