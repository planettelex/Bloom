using System;
using System.Collections.Generic;
using System.IO;
using Bloom.Domain.Models;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the library connection repository class.
    /// </summary>
    [TestFixture]
    public class LibraryConnectionRespositoryTests
    {
        private const string TestFileName = "LibraryConnectionRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private ILibraryConnectionRepository _libraryConnectionRepository;
        private Guid _person1Id;
        private Guid _library1Id;
        private Guid _person2Id;
        private Guid _library2Id;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            _libraryConnectionRepository = new LibraryConnectionRepository(_dataSource);

            var testFolder = Bloom.Data.Settings.TestsDataPath;
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
            var connections = new List<LibraryConnection>();
            var person1 = Person.Create("Person One");
            _person1Id = person1.Id;
            var library1 = Library.Create(person1, "Test One", "c:\\libraries\\");
            _library1Id = library1.Id;
            var connection1 = LibraryConnection.Create(library1);
            connection1.IsConnected = true;
            connection1.LastConnected = DateTime.Now.AddHours(-1);
            connection1.LastConnectionBy = person1.Id;
            connections.Add(connection1);

            var person2 = Person.Create("Person Two");
            _person2Id = person2.Id;
            var library2 = Library.Create(person2, "Test Two", "d:\\");
            _library2Id = library2.Id;
            var connection2 = LibraryConnection.Create(library2);
            connection2.IsConnected = false;
            connection2.LastConnected = DateTime.Now.AddDays(-15);
            connection2.LastConnectionBy = person2.Id;
            connections.Add(connection2);

            _libraryConnectionRepository.AddLibraryConnections(connections);
            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get library connection method.
        /// </summary>
        [Test]
        public void GetLibraryConnectionTest()
        {
            var connection = _libraryConnectionRepository.GetLibraryConnection(_library1Id);
            Assert.NotNull(connection);
            Assert.AreEqual(_library1Id, connection.LibraryId);
            Assert.AreEqual("Test One", connection.LibraryName);
            Assert.AreEqual("c:\\libraries\\Test One.blm", connection.FilePath);
            Assert.AreEqual("Person One", connection.OwnerName);
            Assert.AreEqual(_person1Id, connection.OwnerId);
            Assert.IsTrue(connection.IsConnected);
            Assert.AreEqual(_person1Id, connection.LastConnectionBy);
            Assert.Less(DateTime.Now.AddHours(-2), connection.LastConnected);
            Assert.Greater(DateTime.Now, connection.LastConnected);
        }

        /// <summary>
        /// Tests the get library connection by file path method.
        /// </summary>
        [Test]
        public void GetLibraryConnectionByFilePathTest()
        {
            var connection = _libraryConnectionRepository.GetLibraryConnection("c:\\libraries\\Test One.blm");
            Assert.NotNull(connection);
            Assert.AreEqual(_library1Id, connection.LibraryId);
            Assert.AreEqual("Test One", connection.LibraryName);
            Assert.AreEqual("c:\\libraries\\Test One.blm", connection.FilePath);
            Assert.AreEqual("Person One", connection.OwnerName);
            Assert.AreEqual(_person1Id, connection.OwnerId);
            Assert.IsTrue(connection.IsConnected);
            Assert.AreEqual(_person1Id, connection.LastConnectionBy);
            Assert.Less(DateTime.Now.AddHours(-2), connection.LastConnected);
            Assert.Greater(DateTime.Now, connection.LastConnected);
        }

        /// <summary>
        /// Tests the list library connections method.
        /// </summary>
        [Test]
        public void ListLibraryConnectionsTest()
        {
            var connections = _libraryConnectionRepository.ListLibraryConnections();
            Assert.NotNull(connections);
            Assert.AreEqual(2, connections.Count);
            Assert.AreEqual(_library1Id, connections[0].LibraryId);
            Assert.AreEqual(_library2Id, connections[1].LibraryId);

            var connection = connections[1];
            Assert.NotNull(connection);
            Assert.AreEqual(_library2Id, connection.LibraryId);
            Assert.AreEqual("Test Two", connection.LibraryName);
            Assert.AreEqual("d:\\Test Two.blm", connection.FilePath);
            Assert.AreEqual("Person Two", connection.OwnerName);
            Assert.AreEqual(_person2Id, connection.OwnerId);
            Assert.IsFalse(connection.IsConnected);
            Assert.AreEqual(_person2Id, connection.LastConnectionBy);
            Assert.Less(DateTime.Now.AddDays(-16), connection.LastConnected);
            Assert.Greater(DateTime.Now.AddDays(-14), connection.LastConnected);
        }

        /// <summary>
        /// Tests the list library connections by connected method.
        /// </summary>
        [Test]
        public void ListLibraryConnectionsByConnectedTest()
        {
            var connections = _libraryConnectionRepository.ListLibraryConnections(true);
            Assert.NotNull(connections);
            Assert.AreEqual(1, connections.Count);
            Assert.AreEqual(_library1Id, connections[0].LibraryId);

            connections = _libraryConnectionRepository.ListLibraryConnections(false);
            Assert.NotNull(connections);
            Assert.AreEqual(1, connections.Count);
            Assert.AreEqual(_library2Id, connections[0].LibraryId);
        }

        /// <summary>
        /// Tests the library connection exists method.
        /// </summary>
        [Test]
        public void LibraryConnectionExistsTest()
        {
            Assert.IsTrue(_libraryConnectionRepository.LibraryConnectionExists(_library1Id));
            Assert.IsTrue(_libraryConnectionRepository.LibraryConnectionExists(_library2Id));
            Assert.IsFalse(_libraryConnectionRepository.LibraryConnectionExists(Guid.NewGuid()));
        }

        /// <summary>
        /// Tests the delete library connection method.
        /// </summary>
        [Test]
        public void DeleteLibraryConnectionTest()
        {
            var person3 = Person.Create("Person Three");
            var library3 = Library.Create(person3, "Test Three", "c:\\libraries\\tests");
            var connection3 = LibraryConnection.Create(library3);
            connection3.IsConnected = true;
            connection3.LastConnected = DateTime.Now;
            connection3.LastConnectionBy = person3.Id;

            _libraryConnectionRepository.AddLibraryConnection(connection3);
            _dataSource.Save();
            Assert.IsTrue(_libraryConnectionRepository.LibraryConnectionExists(library3.Id));

            var newConnection = _libraryConnectionRepository.GetLibraryConnection(library3.Id);
            Assert.AreEqual("Test Three", newConnection.LibraryName);

            _libraryConnectionRepository.DeleteLibraryConnection(newConnection);
            _dataSource.Save();
            Assert.IsFalse(_libraryConnectionRepository.LibraryConnectionExists(library3.Id));

            var deletedConnection = _libraryConnectionRepository.GetLibraryConnection(library3.Id);
            Assert.IsNull(deletedConnection);
        }
    }
}
