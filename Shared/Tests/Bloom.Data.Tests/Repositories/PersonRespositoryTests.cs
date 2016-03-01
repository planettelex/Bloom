using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the person repository class.
    /// </summary>
    [TestFixture]
    public class PersonRespositoryTests
    {
        private const string TestFileName = "PersonRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IPersonRepository _personRepository;
        private Guid _johnId;
        private Guid _paulId;
        private Guid _georgeId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _personRepository = new PersonRepository(new PhotoRespository());

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
            var johnLennon = Person.Create("John Lennon");
            _johnId = johnLennon.Id;
            johnLennon.BornOn = DateTime.Parse("10/9/1940");
            johnLennon.DiedOn = DateTime.Parse("12/8/1980");
            johnLennon.Twitter = "@johnlennon";
            johnLennon.Bio = "John's bio";
            var johnPhoto = Photo.Create("c:\\images\\profiles\\john-lennon.jpg");
            johnLennon.ProfileImage = johnPhoto;

            _personRepository.AddPerson(_dataSource, johnLennon);
            _personRepository.AddPersonPhoto(_dataSource, johnLennon, johnPhoto, 1);

            var paulMccartney = Person.Create("Paul McCartney");
            _paulId = paulMccartney.Id;
            paulMccartney.BornOn = DateTime.Parse("6/18/1942");
            paulMccartney.Twitter = "@PaulMcCartney";
            paulMccartney.Bio = "Paul's bio";
            var paulPhoto = Photo.Create("c:\\images\\profiles\\paul-mccartney.jpg");
            paulMccartney.ProfileImage = paulPhoto;

            _personRepository.AddPerson(_dataSource, paulMccartney);
            _personRepository.AddPersonPhoto(_dataSource, paulMccartney, paulPhoto, 1);

            var georgeHarrison = Person.Create("George Harrison");
            _georgeId = georgeHarrison.Id;
            georgeHarrison.BornOn = DateTime.Parse("2/25/1943");
            georgeHarrison.DiedOn = DateTime.Parse("11/29/2001");
            georgeHarrison.Twitter = "@GeorgeHarrison";
            georgeHarrison.Bio = "George's bio";
            var georgePhoto1 = Photo.Create("c:\\images\\profiles\\george-harrison1.jpg");
            var georgePhoto2 = Photo.Create("c:\\images\\profiles\\george-harrison2.jpg");
            georgeHarrison.ProfileImage = georgePhoto1;
            georgeHarrison.Photos.Add(georgePhoto2);

            _personRepository.AddPerson(_dataSource, georgeHarrison);
            _personRepository.AddPersonPhoto(_dataSource, georgeHarrison, georgePhoto1, 1);
            _personRepository.AddPersonPhoto(_dataSource, georgeHarrison, georgePhoto2, 2);
        }

        /// <summary>
        /// Tests the person exists method.
        /// </summary>
        [Test]
        public void PersonExistsTest()
        {
            Assert.IsFalse(_personRepository.PersonExists(_dataSource, Guid.NewGuid()));
            Assert.IsTrue(_personRepository.PersonExists(_dataSource, _johnId));
        }

        /// <summary>
        /// Tests the get person method.
        /// </summary>
        [Test]
        public void GetPersonTest()
        {
            var george = _personRepository.GetPerson(_dataSource, _georgeId);
            Assert.NotNull(george);
            Assert.AreEqual(_georgeId, george.Id);
            Assert.AreEqual("George Harrison", george.Name);
            Assert.AreEqual(DateTime.Parse("2/25/1943"), george.BornOn);
            Assert.AreEqual(DateTime.Parse("11/29/2001"), george.DiedOn);
            Assert.AreEqual("@GeorgeHarrison", george.Twitter);
            Assert.AreEqual("George's bio", george.Bio);
            Assert.NotNull(george.Photos);
            Assert.NotNull(george.ProfileImage);
            Assert.AreEqual(2, george.Photos.Count);
            Assert.AreEqual(george.ProfileImage.Id, george.Photos[0].Id);
            Assert.AreEqual("c:\\images\\profiles\\george-harrison1.jpg", george.Photos[0].FilePath);
            Assert.AreEqual("c:\\images\\profiles\\george-harrison2.jpg", george.Photos[1].FilePath);

            var paul = _personRepository.GetPerson(_dataSource, _paulId);
            Assert.NotNull(paul);
            Assert.AreEqual(_paulId, paul.Id);
            Assert.AreEqual("Paul McCartney", paul.Name);
            Assert.AreEqual(DateTime.Parse("6/18/1942"), paul.BornOn);
            Assert.IsNull(paul.DiedOn);
            Assert.AreEqual("@PaulMcCartney", paul.Twitter);
            Assert.AreEqual("Paul's bio", paul.Bio);
            Assert.NotNull(paul.Photos);
            Assert.NotNull(paul.ProfileImage);
            Assert.AreEqual(1, paul.Photos.Count);
            Assert.AreEqual(paul.ProfileImage.Id, paul.Photos[0].Id);
            Assert.AreEqual("c:\\images\\profiles\\paul-mccartney.jpg", paul.Photos[0].FilePath);

            var john = _personRepository.GetPerson(_dataSource, _johnId);
            Assert.NotNull(john);
            Assert.AreEqual(_johnId, john.Id);
            Assert.AreEqual("John Lennon", john.Name);
            Assert.AreEqual(DateTime.Parse("10/9/1940"), john.BornOn);
            Assert.AreEqual(DateTime.Parse("12/8/1980"), john.DiedOn);
            Assert.AreEqual("@johnlennon", john.Twitter);
            Assert.AreEqual("John's bio", john.Bio);
            Assert.NotNull(john.Photos);
            Assert.NotNull(john.ProfileImage);
            Assert.AreEqual(1, john.Photos.Count);
            Assert.AreEqual(john.ProfileImage.Id, john.Photos[0].Id);
            Assert.AreEqual("c:\\images\\profiles\\john-lennon.jpg", john.Photos[0].FilePath);
        }

        /// <summary>
        /// Tests the delete person method.
        /// </summary>
        [Test]
        public void DeletePersonTest()
        {
            var ringoStarr = Person.Create("Ringo Starr");
            var ringoId = ringoStarr.Id;
            ringoStarr.BornOn = DateTime.Parse("7/7/1940");
            ringoStarr.Twitter = "@ringostarrmusic";
            ringoStarr.Bio = "Ringo's bio";
            var ringoPhoto = Photo.Create("c:\\images\\profiles\\ringo-starr.jpg");
            ringoStarr.ProfileImage = ringoPhoto;

            _personRepository.AddPerson(_dataSource, ringoStarr);
            _personRepository.AddPersonPhoto(_dataSource, ringoStarr, ringoPhoto, 1);

            var person = _personRepository.GetPerson(_dataSource, ringoId);
            Assert.NotNull(person);

            _personRepository.DeletePerson(_dataSource, person);

            var deletedUser = _personRepository.GetPerson(_dataSource, ringoId);
            Assert.IsNull(deletedUser);
        }

        /// <summary>
        /// Tests the delete person photo method.
        /// </summary>
        [Test]
        public void DeletePersonPhotoTest()
        {
            var john = _personRepository.GetPerson(_dataSource, _johnId);
            Assert.NotNull(john);

            var johnPhoto2 = Photo.Create("c:\\images\\profiles\\john-lennon-2.jpg");
            _personRepository.AddPersonPhoto(_dataSource, john, johnPhoto2, 2);

            john = _personRepository.GetPerson(_dataSource, _johnId);
            Assert.AreEqual(2, john.Photos.Count);
            Assert.AreEqual("c:\\images\\profiles\\john-lennon-2.jpg", john.Photos[1].FilePath);

            _personRepository.DeletePersonPhoto(_dataSource, john, john.Photos[1]);
            john = _personRepository.GetPerson(_dataSource, _johnId);
            Assert.AreEqual(1, john.Photos.Count);
        }
    }
}
