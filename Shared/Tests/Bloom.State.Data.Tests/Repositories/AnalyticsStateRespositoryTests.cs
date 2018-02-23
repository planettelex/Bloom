using System;
using System.IO;
using System.Windows;
using Bloom.Domain.Models;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the analytics state repository class.
    /// </summary>
    [TestFixture]
    public class AnalyticsStateRespositoryTests
    {
        private const string TestFileName = "AnalyticsStateRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private IAnalyticsStateRepository _analyticsStateRepository;
        private User _user;
        private Guid _selectedTabId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _user = User.Create(Person.Create("Test User"));
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            var connectionRepository = new LibraryConnectionRepository(_dataSource);
            var tabRepository = new TabRepository(_dataSource);
            _analyticsStateRepository = new AnalyticsStateRepository(_dataSource, connectionRepository, tabRepository);

            var testFolder = Bloom.Data.Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            Assert.IsFalse(_analyticsStateRepository.AnalyticsStateExists(_user));
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            _selectedTabId = Guid.NewGuid();
            var analyticsState = new AnalyticsState
            {
                ProcessName = "Analytics",
                SkinName = "Skin",
                SelectedTabId = _selectedTabId,
                SidebarVisible = true,
                SidebarWidth = 500,
                WindowState = WindowState.Maximized,
                UserId = _user.PersonId,
                User = _user
            };

            _analyticsStateRepository.AddAnalyticsState(analyticsState);
            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get analytics state method.
        /// </summary>
        [Test]
        public void GetAnalyticsStateTest()
        {
            var analyticsState = _analyticsStateRepository.GetAnalyticsState(_user);
            Assert.NotNull(analyticsState);
            Assert.AreEqual("Analytics", analyticsState.ProcessName);
            Assert.AreEqual("Skin", analyticsState.SkinName);
            Assert.AreEqual(_selectedTabId, analyticsState.SelectedTabId);
            Assert.IsTrue(analyticsState.SidebarVisible);
            Assert.AreEqual(500, analyticsState.SidebarWidth);
            Assert.AreEqual(WindowState.Maximized, analyticsState.WindowState);
            Assert.AreEqual(_user.PersonId, analyticsState.UserId);
        }

        /// <summary>
        /// Tests the analytics state exists method.
        /// </summary>
        [Test]
        public void AnalyticsStateExistsTest()
        {
            Assert.IsTrue(_analyticsStateRepository.AnalyticsStateExists(_user));
        }
    }
}
