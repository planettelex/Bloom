using System.IO;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests
{
    /// <summary>
    /// Tests the state data source class.
    /// </summary>
    [TestFixture]
    public class StateDataSourceTests
    {
        private const string TestFileName = "StateDataSourceTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private string _testFolder;

        /// <summary>
        /// Sets up the tests by creating a test data source.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);

            _testFolder = Bloom.Data.Settings.TestsDataPath;
            if (!Directory.Exists(_testFolder))
                Directory.CreateDirectory(_testFolder);

            var testFilePath = Path.Combine(_testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
            
            _dataSource.Create(testFilePath);
        }

        /// <summary>
        /// Tests the data source properties.
        /// </summary>
        [Test]
        public void PropertiesTest()
        {
            Assert.NotNull(_dataSource.Context);
            Assert.AreEqual(Path.Combine(_testFolder, TestFileName), _dataSource.FilePath);
        }

        /// <summary>
        /// Tests the register repositories method.
        /// </summary>
        [Test]
        public void RegisterRepositoriesTest()
        {
            _container.RegisterType<IDataSource, StateDataSource>(new ContainerControlledLifetimeManager());
            _container.Resolve<IDataSource>();
            _dataSource.RegisterRepositories();

            Assert.NotNull(_container.Resolve<ISuiteStateRepository>());
            Assert.NotNull(_container.Resolve<ILibraryConnectionRepository>());
            Assert.NotNull(_container.Resolve<IUserRepository>());
            Assert.NotNull(_container.Resolve<ITabRepository>());
            Assert.NotNull(_container.Resolve<IAnalyticsStateRepository>());
            Assert.NotNull(_container.Resolve<IBrowserStateRepository>());
            Assert.NotNull(_container.Resolve<IPlayerStateRepository>());
        }

        /// <summary>
        /// Tests connecting and disconnecting from the data source.
        /// </summary>
        [Test]
        public void ConnectionTest()
        {
            Assert.IsTrue(File.Exists(_dataSource.FilePath));
            Assert.IsTrue(_dataSource.IsConnected());
            Assert.IsNotNull(_dataSource.Context);
            
            _dataSource.Disconnect();
            Assert.IsFalse(_dataSource.IsConnected());
            Assert.IsNull(_dataSource.Context);

            _dataSource.Connect();
            Assert.IsTrue(_dataSource.IsConnected());
            Assert.IsNotNull(_dataSource.Context);
        }
    }
}
