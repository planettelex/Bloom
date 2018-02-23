using System;
using System.IO;
using Bloom.Domain.Models;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the user repository class.
    /// </summary>
    [TestFixture]
    public class UserRespositoryTests
    {
        private const string TestFileName = "UserRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private IUserRepository _userRepository;
        private Guid _thomId;
        private Guid _jonnyId;
        private Guid _colinId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            _userRepository = new UserRepository(_dataSource);

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
            var thomYork = User.Create(Person.Create("Thom York"));
            _thomId = thomYork.PersonId;
            thomYork.Birthday = DateTime.Parse("10/7/1968");
            thomYork.Twitter = "@thomyorke";
            thomYork.ProfileImagePath = "c:\\images\\profiles\\thom-yorke.jpg";
            thomYork.LastLogin = DateTime.Now.AddDays(-7);
            _userRepository.AddUser(thomYork);

            var jonnyGreenwood = User.Create(Person.Create("Jonny Greenwood"));
            _jonnyId = jonnyGreenwood.PersonId;
            jonnyGreenwood.Birthday = DateTime.Parse("11/5/1971");
            jonnyGreenwood.Twitter = "@JnnyG";
            jonnyGreenwood.ProfileImagePath = "c:\\images\\profiles\\jonny-greenwood.jpg";
            jonnyGreenwood.LastLogin = DateTime.Now.AddDays(-9);
            _userRepository.AddUser(jonnyGreenwood);

            var colinGreenwood = User.Create(Person.Create("Colin Greenwood"));
            _colinId = colinGreenwood.PersonId;
            colinGreenwood.Birthday = DateTime.Parse("6/26/1969");
            colinGreenwood.Twitter = "@colingreenwood";
            colinGreenwood.ProfileImagePath = "c:\\images\\profiles\\colin-greenwood.jpg";
            colinGreenwood.LastLogin = DateTime.Now.AddDays(-11);
            _userRepository.AddUser(colinGreenwood);

            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get user method.
        /// </summary>
        [Test]
        public void GetUserTest()
        {
            var user = _userRepository.GetUser(_jonnyId);
            Assert.NotNull(user);
            Assert.AreEqual(_jonnyId, user.PersonId);
            Assert.AreEqual("Jonny Greenwood", user.Name);
            Assert.AreEqual(DateTime.Parse("11/5/1971"), user.Birthday);
            Assert.AreEqual("@JnnyG", user.Twitter);
            Assert.AreEqual("c:\\images\\profiles\\jonny-greenwood.jpg", user.ProfileImagePath);
            Assert.Less(DateTime.Now.AddDays(-10), user.LastLogin);
            Assert.Greater(DateTime.Now.AddDays(-8), user.LastLogin);
        }

        /// <summary>
        /// Tests the get last user method.
        /// </summary>
        [Test]
        public void GetLastUserTest()
        {
            var user = _userRepository.GetLastUser();
            Assert.NotNull(user);
            Assert.AreEqual(_thomId, user.PersonId);
            Assert.AreEqual("Thom York", user.Name);
            Assert.AreEqual(DateTime.Parse("10/7/1968"), user.Birthday);
            Assert.AreEqual("@thomyorke", user.Twitter);
            Assert.AreEqual("c:\\images\\profiles\\thom-yorke.jpg", user.ProfileImagePath);
            Assert.Less(DateTime.Now.AddDays(-8), user.LastLogin);
            Assert.Greater(DateTime.Now.AddDays(-6), user.LastLogin);
        }

        /// <summary>
        /// Tests the list users method.
        /// </summary>
        [Test]
        public void ListUsersTest()
        {
            var users = _userRepository.ListUsers();
            Assert.NotNull(users);
            Assert.AreEqual(3, users.Count);
            Assert.AreEqual(_thomId, users[0].PersonId);
            Assert.AreEqual(_jonnyId, users[1].PersonId);
            Assert.AreEqual(_colinId, users[2].PersonId);

            var user = users[2];
            Assert.NotNull(user);
            Assert.AreEqual(_colinId, user.PersonId);
            Assert.AreEqual("Colin Greenwood", user.Name);
            Assert.AreEqual(DateTime.Parse("6/26/1969"), user.Birthday);
            Assert.AreEqual("@colingreenwood", user.Twitter);
            Assert.AreEqual("c:\\images\\profiles\\colin-greenwood.jpg", user.ProfileImagePath);
            Assert.Less(DateTime.Now.AddDays(-12), user.LastLogin);
            Assert.Greater(DateTime.Now.AddDays(-10), user.LastLogin);
        }

        /// <summary>
        /// Tests the delete user method.
        /// </summary>
        [Test]
        public void DeleteUserTest()
        {
            var edObrien = User.Create(Person.Create("Ed O'Brian"));
            var edId = edObrien.PersonId;
            edObrien.Birthday = DateTime.Parse("4/15/1968");
            edObrien.Twitter = "@edfromradiohead";
            edObrien.ProfileImagePath = "c:\\images\\profiles\\ed-obrian.jpg";
            edObrien.LastLogin = DateTime.Now.AddDays(-3);
            _userRepository.AddUser(edObrien);
            _dataSource.Save();

            var users = _userRepository.ListUsers();
            Assert.NotNull(users);
            Assert.AreEqual(4, users.Count);

            var user = _userRepository.GetUser(edId);
            Assert.NotNull(user);

            _userRepository.DeleteUser(user);
            _dataSource.Save();

            users = _userRepository.ListUsers();
            Assert.NotNull(users);
            Assert.AreEqual(3, users.Count);

            var deletedUser = _userRepository.GetUser(edId);
            Assert.IsNull(deletedUser);
        }
    }
}
