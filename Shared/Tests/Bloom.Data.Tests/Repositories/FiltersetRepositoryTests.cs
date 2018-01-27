using System;
using System.Collections.Generic;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the filterset repository class.
    /// </summary>
    [TestFixture]
    public class FiltersetRepositoryTests
    {
        private const string TestFileName = "FiltersetRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IFiltersetRepository _filtersetRepository;
        private Guid _filterset1Id;

        #region Test Classes

        /// <summary>
        /// A test filter.
        /// </summary>
        /// <seealso cref="Bloom.Domain.Interfaces.IFilter" />
        private class TestFilter1 : IFilter
        {
            /// <summary>
            /// 265e69c3-8d01-47e5-a349-8414abb2de34
            /// </summary>
            public Guid Id => Guid.Parse("265e69c3-8d01-47e5-a349-8414abb2de34");

            /// <summary>
            /// Test Filter Label 1
            /// </summary>
            public string Label => "Test Filter Label 1";

            public List<T> Apply<T>(List<T> items, FilterComparison comparison, string compareAgainst)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Another test filter.
        /// </summary>
        /// <seealso cref="Bloom.Domain.Interfaces.IFilter" />
        private class TestFilter2 : IFilter
        {
            /// <summary>
            /// 722c0722-9408-4d4b-9416-272c3b61c583
            /// </summary>
            public Guid Id => Guid.Parse("722c0722-9408-4d4b-9416-272c3b61c583");

            /// <summary>
            /// Test Filter Label 2
            /// </summary>
            public string Label => "Test Filter Label 2";

            public List<T> Apply<T>(List<T> items, FilterComparison comparison, string compareAgainst)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// A test order.
        /// </summary>
        /// <seealso cref="Bloom.Domain.Interfaces.IOrder" />
        private class TestOrder1 : IOrder
        {
            /// <summary>
            /// e18e5ab2-6aaf-4bae-8fac-5da2a876f3e8
            /// </summary>
            public Guid Id => Guid.Parse("e18e5ab2-6aaf-4bae-8fac-5da2a876f3e8");

            /// <summary>
            /// Test Order Label 1
            /// </summary>
            public string Label => "Test Order Label 1";
        }

        /// <summary>
        /// Another test order.
        /// </summary>
        /// <seealso cref="Bloom.Domain.Interfaces.IOrder" />
        private class TestOrder2 : IOrder
        {
            /// <summary>
            /// 6e3cb734-d7ce-4043-b2fb-d9480f3601d0
            /// </summary>
            public Guid Id => Guid.Parse("6e3cb734-d7ce-4043-b2fb-d9480f3601d0");

            /// <summary>
            /// Test Order Label 2
            /// </summary>
            public string Label => "Test Order Label 2";
        }

        #endregion

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _filtersetRepository = new FiltersetRepository();

            var testFolder = Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            var filterset1 = Filterset.Create("Filterset 1");
            _filterset1Id = filterset1.Id;
            _filtersetRepository.AddFilterset(_dataSource, filterset1);

            var filterset1Element1 = FiltersetExpressionElement.Create(filterset1, new TestFilter1(), 1);
            filterset1Element1.Comparison = FilterComparison.Is;
            filterset1Element1.FilterAgainst = "Filter Element 1 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element1);
            var filterset1Element2 = FiltersetExpressionElement.Create(filterset1, FiltersetElementType.And, 2);
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element2);
            var filterset1Element3 = FiltersetExpressionElement.Create(filterset1, new TestFilter2(), 3);
            filterset1Element3.Comparison = FilterComparison.DoesNotContain;
            filterset1Element3.FilterAgainst = "Filter Element 3 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element3);

            var filterset1Order1 = FiltersetOrderingElement.Create(filterset1, new TestOrder1(), 1);
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset1Order1);
            var filterset1Order2 = FiltersetOrderingElement.Create(filterset1, new TestOrder2(), 2);
            filterset1Order2.Direction = OrderDirection.Descending;
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset1Order2);

            var filterset2 = Filterset.Create("Filterset 2");
            _filtersetRepository.AddFilterset(_dataSource, filterset2);
            var filterset2Element1 = FiltersetExpressionElement.Create(filterset2, FiltersetElementType.OpenParenthesis, 1);
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset2Element1);
            var filterset2Element2 = FiltersetExpressionElement.Create(filterset2, new TestFilter1(), 2);
            filterset2Element2.Comparison = FilterComparison.EndsWith;
            filterset2Element2.FilterAgainst = "Filter Element 2 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset2Element2);
            var filterset2Element3 = FiltersetExpressionElement.Create(filterset2, FiltersetElementType.CloseParenthesis, 3);
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset2Element3);
            
            var filterset3 = Filterset.Create("Filterset 3");
            _filtersetRepository.AddFilterset(_dataSource, filterset3);
        }

        /// <summary>
        /// Tests the get filerset method.
        /// </summary>
        [Test]
        public void GetFiltersetTest()
        {
            var filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.NotNull(filterset);
            Assert.AreEqual(_filterset1Id, filterset.Id);
            Assert.AreEqual("Filterset 1", filterset.Name);
            Assert.LessOrEqual(filterset.CreatedOn, DateTime.Now);
            Assert.Greater(filterset.CreatedOn, DateTime.Now.AddMinutes(-1));
            Assert.NotNull(filterset.AlbumFilterExpression);
            Assert.AreEqual(3, filterset.AlbumFilterExpression.Count);
            Assert.AreEqual(1, filterset.AlbumFilterExpression[0].ElementNumber);
            Assert.AreEqual(FiltersetElementType.Filter, filterset.AlbumFilterExpression[0].ElementType);
            Assert.AreEqual(FilterComparison.Is, filterset.AlbumFilterExpression[0].Comparison);
            Assert.AreEqual("Filter Element 1 Against", filterset.AlbumFilterExpression[0].FilterAgainst);
            Assert.AreEqual(new TestFilter1().Id, filterset.AlbumFilterExpression[0].FilterId);
            Assert.AreEqual(2, filterset.AlbumFilterExpression[1].ElementNumber);
            Assert.AreEqual(FiltersetElementType.And, filterset.AlbumFilterExpression[1].ElementType);
            Assert.AreEqual(3, filterset.AlbumFilterExpression[2].ElementNumber);
            Assert.AreEqual(FiltersetElementType.Filter, filterset.AlbumFilterExpression[2].ElementType);
            Assert.AreEqual(FilterComparison.DoesNotContain, filterset.AlbumFilterExpression[2].Comparison);
            Assert.AreEqual("Filter Element 3 Against", filterset.AlbumFilterExpression[2].FilterAgainst);
            Assert.AreEqual(new TestFilter2().Id, filterset.AlbumFilterExpression[2].FilterId);
            Assert.NotNull(filterset.AlbumOrdering);
            Assert.AreEqual(2, filterset.AlbumOrdering.Count);
            Assert.AreEqual(1, filterset.AlbumOrdering[0].OrderNumber);
            Assert.AreEqual(OrderDirection.Ascending, filterset.AlbumOrdering[0].Direction);
            Assert.AreEqual(new TestOrder1().Id, filterset.AlbumOrdering[0].OrderId);
            Assert.AreEqual(2, filterset.AlbumOrdering[1].OrderNumber);
            Assert.AreEqual(OrderDirection.Descending, filterset.AlbumOrdering[1].Direction);
            Assert.AreEqual(new TestOrder2().Id, filterset.AlbumOrdering[1].OrderId);
        }

        /// <summary>
        /// Tests the list filtersets method.
        /// </summary>
        [Test]
        public void ListFiltersetsTest()
        {
            var filtersets = _filtersetRepository.ListFiltersets(_dataSource);
            Assert.NotNull(filtersets);
            Assert.AreEqual(3, filtersets.Count);
            Assert.AreEqual(_filterset1Id, filtersets[0].Id);
            Assert.AreEqual("Filterset 1", filtersets[0].Name);
            Assert.LessOrEqual(filtersets[0].CreatedOn, DateTime.Now);
            Assert.Greater(filtersets[0].CreatedOn, DateTime.Now.AddMinutes(-1));
            Assert.AreEqual("Filterset 2", filtersets[1].Name);
            Assert.AreEqual("Filterset 3", filtersets[2].Name);
        }

        /// <summary>
        /// Tests the delete filterset element method.
        /// </summary>
        [Test]
        public void DeleteFiltersetElementTest()
        {
            var filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.NotNull(filterset);
            Assert.NotNull(filterset.AlbumFilterExpression);
            Assert.AreEqual(3, filterset.AlbumFilterExpression.Count);
            var filterset1Element4 = FiltersetExpressionElement.Create(filterset, new TestFilter2(), 4);
            filterset1Element4.Comparison = FilterComparison.IsInTheLast;
            filterset1Element4.FilterAgainst = "Filter Element 4 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element4);

            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(4, filterset.AlbumFilterExpression.Count);
            Assert.AreEqual("Filter Element 4 Against", filterset.AlbumFilterExpression[3].FilterAgainst);

            _filtersetRepository.DeleteFiltersetElement(_dataSource, filterset.AlbumFilterExpression[3]);
            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(3, filterset.AlbumFilterExpression.Count);
        }

        /// <summary>
        /// Tests the delete filterset order method.
        /// </summary>
        [Test]
        public void DeleteFiltersetOrderTest()
        {
            var filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.NotNull(filterset);
            Assert.NotNull(filterset.AlbumOrdering);
            Assert.AreEqual(2, filterset.AlbumOrdering.Count);
            var filterset1Order3 = FiltersetOrderingElement.Create(filterset, new TestOrder2(), 3);
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset1Order3);

            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(3, filterset.AlbumOrdering.Count);
            Assert.AreEqual(filterset1Order3.OrderId, filterset.AlbumOrdering[2].OrderId);

            _filtersetRepository.DeleteFiltersetOrder(_dataSource, filterset.AlbumOrdering[2]);
            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(2, filterset.AlbumOrdering.Count);
        }

        /// <summary>
        /// Tests the delete filterset method.
        /// </summary>
        [Test]
        public void DeleteFilterset()
        {
            var filterset4 = Filterset.Create("Filterset 4");
            _filtersetRepository.AddFilterset(_dataSource, filterset4);
            var filterset4Element1 = FiltersetExpressionElement.Create(filterset4, new TestFilter1(), 1);
            filterset4Element1.Comparison = FilterComparison.BeginsWith;
            filterset4Element1.FilterAgainst = "Filter Element 4 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset4Element1);
            var filterset4Order1 = FiltersetOrderingElement.Create(filterset4, new TestOrder1(), 1);
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset4Order1);

            var filterset = _filtersetRepository.GetFilterset(_dataSource, filterset4.Id);
            Assert.NotNull(filterset);
            Assert.AreEqual("Filterset 4", filterset.Name);
            Assert.NotNull(filterset.AlbumFilterExpression);
            Assert.AreEqual(1, filterset.AlbumFilterExpression.Count);
            Assert.NotNull(filterset.AlbumOrdering);
            Assert.AreEqual(1, filterset.AlbumOrdering.Count);

            _filtersetRepository.DeleteFilterset(_dataSource, filterset4);
            filterset = _filtersetRepository.GetFilterset(_dataSource, filterset4.Id);
            Assert.IsNull(filterset);
        }
    }
}
