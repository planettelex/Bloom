using System.IO;
using Bloom.Data.Interfaces;
using Bloom.Data.Repositories;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests
{
    /// <summary>
    /// Tests the library data source class.
    /// </summary>
    [TestFixture]
    public class LibraryDateSourceTests
    {
        private const string TestFileName = "LibraryDataSourceTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private string _testFolder;

        /// <summary>
        /// Sets up the tests by creating a test data source.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);

            _testFolder = Settings.TestsDataPath;
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
            _container.RegisterType<IDataSource, LibraryDataSource>(new ContainerControlledLifetimeManager());
            _container.Resolve<IDataSource>();
            _dataSource.RegisterRepositories();

            Assert.NotNull(_container.Resolve<IRoleRepository>());
            Assert.NotNull(_container.Resolve<ISourceRepository>());
            Assert.NotNull(_container.Resolve<IPhotoRespository>());
            Assert.NotNull(_container.Resolve<IPersonRepository>());
            Assert.NotNull(_container.Resolve<IArtistRepository>());
            Assert.NotNull(_container.Resolve<IHolidayRepository>());
            Assert.NotNull(_container.Resolve<IGenreRepository>());
            Assert.NotNull(_container.Resolve<ITimeSignatureRepository>());
            Assert.NotNull(_container.Resolve<ILibraryRepository>());
            Assert.NotNull(_container.Resolve<IAlbumRepository>());
            Assert.NotNull(_container.Resolve<IFiltersetRepository>());
            Assert.NotNull(_container.Resolve<ILabelRepository>());
            Assert.NotNull(_container.Resolve<IPlaylistRepository>());
            Assert.NotNull(_container.Resolve<ISongRepository>());
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
