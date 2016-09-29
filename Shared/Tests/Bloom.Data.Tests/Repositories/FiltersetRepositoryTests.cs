using System;
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
            public Guid Id { get { return Guid.Parse("265e69c3-8d01-47e5-a349-8414abb2de34"); } }

            /// <summary>
            /// Test Filter Label 1
            /// </summary>
            public string Label { get { return "Test Filter Label 1"; } }
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
            public Guid Id { get { return Guid.Parse("722c0722-9408-4d4b-9416-272c3b61c583"); } }

            /// <summary>
            /// Test Filter Label 2
            /// </summary>
            public string Label { get { return "Test Filter Label 2"; } }
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
            public Guid Id { get { return Guid.Parse("e18e5ab2-6aaf-4bae-8fac-5da2a876f3e8"); } }

            /// <summary>
            /// Test Order Label 1
            /// </summary>
            public string Label { get { return "Test Order Label 1"; } }
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
            public Guid Id { get { return Guid.Parse("6e3cb734-d7ce-4043-b2fb-d9480f3601d0"); } }

            /// <summary>
            /// Test Order Label 2
            /// </summary>
            public string Label { get { return "Test Order Label 2"; } }
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

            var filterset1Element1 = FiltersetElement.Create(filterset1, new TestFilter1(), 1);
            filterset1Element1.Comparison = FilterComparison.Is;
            filterset1Element1.FilterAgainst = "Filter Element 1 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element1);
            var filterset1Element2 = FiltersetElement.Create(filterset1, FiltersetElementType.And, 2);
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element2);
            var filterset1Element3 = FiltersetElement.Create(filterset1, new TestFilter2(), 3);
            filterset1Element3.Comparison = FilterComparison.DoesNotContain;
            filterset1Element3.FilterAgainst = "Filter Element 3 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element3);

            var filterset1Order1 = FiltersetOrder.Create(filterset1, new TestOrder1(), 1);
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset1Order1);
            var filterset1Order2 = FiltersetOrder.Create(filterset1, new TestOrder2(), 2);
            filterset1Order2.Direction = OrderDirection.Descending;
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset1Order2);

            var filterset2 = Filterset.Create("Filterset 2");
            _filtersetRepository.AddFilterset(_dataSource, filterset2);
            var filterset2Element1 = FiltersetElement.Create(filterset2, FiltersetElementType.OpenParenthesis, 1);
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset2Element1);
            var filterset2Element2 = FiltersetElement.Create(filterset2, new TestFilter1(), 2);
            filterset2Element2.Comparison = FilterComparison.EndsWith;
            filterset2Element2.FilterAgainst = "Filter Element 2 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset2Element2);
            var filterset2Element3 = FiltersetElement.Create(filterset2, FiltersetElementType.CloseParenthesis, 3);
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
            Assert.NotNull(filterset.Elements);
            Assert.AreEqual(3, filterset.Elements.Count);
            Assert.AreEqual(1, filterset.Elements[0].ElementNumber);
            Assert.AreEqual(FiltersetElementType.Statement, filterset.Elements[0].ElementType);
            Assert.AreEqual(FilterComparison.Is, filterset.Elements[0].Comparison);
            Assert.AreEqual("Filter Element 1 Against", filterset.Elements[0].FilterAgainst);
            Assert.AreEqual(new TestFilter1().Id, filterset.Elements[0].FilterId);
            Assert.AreEqual(2, filterset.Elements[1].ElementNumber);
            Assert.AreEqual(FiltersetElementType.And, filterset.Elements[1].ElementType);
            Assert.AreEqual(3, filterset.Elements[2].ElementNumber);
            Assert.AreEqual(FiltersetElementType.Statement, filterset.Elements[2].ElementType);
            Assert.AreEqual(FilterComparison.DoesNotContain, filterset.Elements[2].Comparison);
            Assert.AreEqual("Filter Element 3 Against", filterset.Elements[2].FilterAgainst);
            Assert.AreEqual(new TestFilter2().Id, filterset.Elements[2].FilterId);
            Assert.NotNull(filterset.Ordering);
            Assert.AreEqual(2, filterset.Ordering.Count);
            Assert.AreEqual(1, filterset.Ordering[0].OrderNumber);
            Assert.AreEqual(OrderDirection.Ascending, filterset.Ordering[0].Direction);
            Assert.AreEqual(new TestOrder1().Id, filterset.Ordering[0].OrderId);
            Assert.AreEqual(2, filterset.Ordering[1].OrderNumber);
            Assert.AreEqual(OrderDirection.Descending, filterset.Ordering[1].Direction);
            Assert.AreEqual(new TestOrder2().Id, filterset.Ordering[1].OrderId);
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
            Assert.NotNull(filterset.Elements);
            Assert.AreEqual(3, filterset.Elements.Count);
            var filterset1Element4 = FiltersetElement.Create(filterset, new TestFilter2(), 4);
            filterset1Element4.Comparison = FilterComparison.IsInTheLast;
            filterset1Element4.FilterAgainst = "Filter Element 4 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset1Element4);

            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(4, filterset.Elements.Count);
            Assert.AreEqual("Filter Element 4 Against", filterset.Elements[3].FilterAgainst);

            _filtersetRepository.DeleteFiltersetElement(_dataSource, filterset.Elements[3]);
            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(3, filterset.Elements.Count);
        }

        /// <summary>
        /// Tests the delete filterset order method.
        /// </summary>
        [Test]
        public void DeleteFiltersetOrderTest()
        {
            var filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.NotNull(filterset);
            Assert.NotNull(filterset.Ordering);
            Assert.AreEqual(2, filterset.Ordering.Count);
            var filterset1Order3 = FiltersetOrder.Create(filterset, new TestOrder2(), 3);
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset1Order3);

            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(3, filterset.Ordering.Count);
            Assert.AreEqual(filterset1Order3.OrderId, filterset.Ordering[2].OrderId);

            _filtersetRepository.DeleteFiltersetOrder(_dataSource, filterset.Ordering[2]);
            filterset = _filtersetRepository.GetFilterset(_dataSource, _filterset1Id);
            Assert.AreEqual(2, filterset.Ordering.Count);
        }

        /// <summary>
        /// Tests the delete filterset method.
        /// </summary>
        [Test]
        public void DeleteFilterset()
        {
            var filterset4 = Filterset.Create("Filterset 4");
            _filtersetRepository.AddFilterset(_dataSource, filterset4);
            var filterset4Element1 = FiltersetElement.Create(filterset4, new TestFilter1(), 1);
            filterset4Element1.Comparison = FilterComparison.BeginsWith;
            filterset4Element1.FilterAgainst = "Filter Element 4 Against";
            _filtersetRepository.AddFiltersetElement(_dataSource, filterset4Element1);
            var filterset4Order1 = FiltersetOrder.Create(filterset4, new TestOrder1(), 1);
            _filtersetRepository.AddFiltersetOrder(_dataSource, filterset4Order1);

            var filterset = _filtersetRepository.GetFilterset(_dataSource, filterset4.Id);
            Assert.NotNull(filterset);
            Assert.AreEqual("Filterset 4", filterset.Name);
            Assert.NotNull(filterset.Elements);
            Assert.AreEqual(1, filterset.Elements.Count);
            Assert.NotNull(filterset.Ordering);
            Assert.AreEqual(1, filterset.Ordering.Count);

            _filtersetRepository.DeleteFilterset(_dataSource, filterset4);
            filterset = _filtersetRepository.GetFilterset(_dataSource, filterset4.Id);
            Assert.IsNull(filterset);
        }
    }
}
