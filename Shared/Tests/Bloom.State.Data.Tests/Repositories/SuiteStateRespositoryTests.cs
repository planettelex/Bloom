using System;
using System.IO;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the suite state repository class.
    /// </summary>
    [TestFixture]
    public class SuiteStateRespositoryTests
    {
        private const string TestFileName = "SuiteStateRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private ISuiteStateRepository _suiteStateRepository;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            _suiteStateRepository = new SuiteStateRepository(_dataSource);

            var testFolder = Bloom.Data.Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            Assert.IsFalse(_suiteStateRepository.SuiteStateExists());
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            var suiteState = new SuiteState
            {
                SuiteName = "Bloom Test",
                LastProcessAccess = "Browser",
                ProcessAccessedOn = DateTime.Now.AddHours(-1)
            };
            _suiteStateRepository.AddSuiteState(suiteState);

            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get suite state method.
        /// </summary>
        [Test]
        public void GetSuiteStateTest()
        {
            var suiteState = _suiteStateRepository.GetSuiteState();
            Assert.NotNull(suiteState);
            Assert.AreEqual("Bloom Test", suiteState.SuiteName);
            Assert.AreEqual("Browser", suiteState.LastProcessAccess);
            Assert.Less(DateTime.Now.AddHours(-2), suiteState.ProcessAccessedOn);
            Assert.Greater(DateTime.Now, suiteState.ProcessAccessedOn);
        }

        /// <summary>
        /// Tests the suite state exists method.
        /// </summary>
        [Test]
        public void SuiteStateExistsTest()
        {
            Assert.IsTrue(_suiteStateRepository.SuiteStateExists());
        }
    }
}
