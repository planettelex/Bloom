using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the role repository class.
    /// </summary>
    [TestFixture]
    public class RoleRespositoryTests
    {
        private const string TestFileName = "RoleRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IRoleRepository _roleRespository;
        private Guid _role1Id;
        private Guid _role2Id;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _roleRespository = new RoleRepository();

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
            var role1 = Role.Create("Lead Guitar");
            _role1Id = role1.Id;
            _roleRespository.AddRole(_dataSource, role1);

            var role2 = Role.Create("Sound Engineer");
            _role2Id = role2.Id;
            _roleRespository.AddRole(_dataSource, role2);
        }

        /// <summary>
        /// Tests the role exists method.
        /// </summary>
        [Test]
        public void RoleExistsTest()
        {
            Assert.IsFalse(_roleRespository.RoleExists(_dataSource, Guid.NewGuid()));
            Assert.IsTrue(_roleRespository.RoleExists(_dataSource, _role1Id));

            Assert.IsFalse(_roleRespository.RoleExists(_dataSource, "Juggler"));
            Assert.IsTrue(_roleRespository.RoleExists(_dataSource, "Sound Engineer"));
        }

        /// <summary>
        /// Tests the get role method.
        /// </summary>
        [Test]
        public void GetRoleTest()
        {
            var role = _roleRespository.GetRole(_dataSource, _role1Id);
            Assert.NotNull(role);
            Assert.AreEqual(_role1Id, role.Id);
            Assert.AreEqual("Lead Guitar", role.Name);

            role = _roleRespository.GetRole(_dataSource, "Sound Engineer");
            Assert.NotNull(role);
            Assert.AreEqual(_role2Id, role.Id);
            Assert.AreEqual("Sound Engineer", role.Name);
        }

        /// <summary>
        /// Tests the delete role method.
        /// </summary>
        [Test]
        public void DeleteRoleTest()
        {
            var role3 = Role.Create("Photographer");
            var role3Id = role3.Id;
            _roleRespository.AddRole(_dataSource, role3);

            var role = _roleRespository.GetRole(_dataSource, role3Id);
            Assert.NotNull(role);

            _roleRespository.DeleteRole(_dataSource, role);

            var deletedRole = _roleRespository.GetRole(_dataSource, role3Id);
            Assert.IsNull(deletedRole);
        }
    }
}
