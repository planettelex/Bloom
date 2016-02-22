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
    /// Tests the browser state repository class.
    /// </summary>
    [TestFixture]
    public class BrowserStateRespositoryTests
    {
        private const string TestFileName = "BrowserStateRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private IBrowserStateRepository _browserStateRepository;
        private User _user;
        private Guid _selectedTabId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _user = User.Create(Person.Create("Test User"));
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            var connectionRepository = new LibraryConnectionRepository(_dataSource);
            var tabRepository = new TabRepository(_dataSource);
            _browserStateRepository = new BrowserStateRepository(_dataSource, connectionRepository, tabRepository);

            var testFolder = Bloom.Data.Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            Assert.IsFalse(_browserStateRepository.BrowserStateExists(_user));
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            _selectedTabId = Guid.NewGuid();
            var analyticsState = new BrowserState
            {
                ProcessName = "Browser",
                SkinName = "Skin",
                SelectedTabId = _selectedTabId,
                SidebarVisible = true,
                SidebarWidth = 500,
                WindowState = WindowState.Maximized,
                UserId = _user.PersonId,
                User = _user
            };

            _browserStateRepository.AddBrowserState(analyticsState);
            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get browser state method.
        /// </summary>
        [Test]
        public void GetBrowserStateTest()
        {
            var analyticsState = _browserStateRepository.GetBrowserState(_user);
            Assert.NotNull(analyticsState);
            Assert.AreEqual("Browser", analyticsState.ProcessName);
            Assert.AreEqual("Skin", analyticsState.SkinName);
            Assert.AreEqual(_selectedTabId, analyticsState.SelectedTabId);
            Assert.IsTrue(analyticsState.SidebarVisible);
            Assert.AreEqual(500, analyticsState.SidebarWidth);
            Assert.AreEqual(WindowState.Maximized, analyticsState.WindowState);
            Assert.AreEqual(_user.PersonId, analyticsState.UserId);
        }

        /// <summary>
        /// Tests the browser state exists method.
        /// </summary>
        [Test]
        public void AnalyticsStateExistsTest()
        {
            Assert.IsTrue(_browserStateRepository.BrowserStateExists(_user));
        }
    }
}
