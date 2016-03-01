using System;
using System.IO;
using System.Linq;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the artist repository class.
    /// </summary>
    [TestFixture]
    public class ArtistRepositoryTests
    {
        private const string TestFileName = "ArtistRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IArtistRepository _artistRepository;
        private Guid _beatlesId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            var photoRepository = new PhotoRespository();
            _roleRepository = new RoleRepository();
            _personRepository = new PersonRepository(photoRepository);
            _artistRepository = new ArtistRepository(_roleRepository, photoRepository, _personRepository);

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
            var beatles = Artist.Create("The Beatles");
            _beatlesId = beatles.Id;
            beatles.Bio = "Beatles bio";
            beatles.Twitter = "@thebeatles";
            beatles.StartedOn = DateTime.Parse("2/15/1958");
            beatles.EndedOn = DateTime.Parse("8/20/1969");

            _artistRepository.AddArtist(_dataSource, beatles);
            _artistRepository.AddArtistPhoto(_dataSource, beatles, Photo.Create("c:\\images\\beatles-1.jpg"), 1);
            _artistRepository.AddArtistPhoto(_dataSource, beatles, Photo.Create("c:\\images\\beatles-2.jpg"), 2);

            var leadVocals = Role.Create("Lead Vocals");
            _roleRepository.AddRole(_dataSource, leadVocals);
            var backingVocals = Role.Create("Backing Vocals");
            _roleRepository.AddRole(_dataSource, backingVocals);
            var rhythmGuitar = Role.Create("Rhythm Guitar");
            _roleRepository.AddRole(_dataSource, rhythmGuitar);
            var leadGuitar = Role.Create("Lead Guitar");
            _roleRepository.AddRole(_dataSource, leadGuitar);
            var bassGuitar = Role.Create("Bass Guitar");
            _roleRepository.AddRole(_dataSource, bassGuitar);
            var drums = Role.Create("Drums");
            _roleRepository.AddRole(_dataSource, drums);

            var johnLennon = Person.Create("John Lennon");
            johnLennon.BornOn = DateTime.Parse("10/9/1940");
            johnLennon.DiedOn = DateTime.Parse("12/8/1980");
            johnLennon.Twitter = "@johnlennon";
            johnLennon.Bio = "John's bio";
            var johnPhoto = Photo.Create("c:\\images\\profiles\\john-lennon.jpg");
            johnLennon.ProfileImage = johnPhoto;
            var johnMember = ArtistMember.Create(beatles, johnLennon, 1);
            johnMember.Started = DateTime.Parse("2/15/1958");
            johnMember.Ended = DateTime.Parse("8/20/1969");

            _personRepository.AddPerson(_dataSource, johnLennon);
            _personRepository.AddPersonPhoto(_dataSource, johnLennon, johnPhoto, 1);
            _artistRepository.AddArtistMember(_dataSource, johnMember);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, rhythmGuitar);

            var paulMccartney = Person.Create("Paul McCartney");
            paulMccartney.BornOn = DateTime.Parse("6/18/1942");
            paulMccartney.Twitter = "@PaulMcCartney";
            paulMccartney.Bio = "Paul's bio";
            var paulPhoto = Photo.Create("c:\\images\\profiles\\paul-mccartney.jpg");
            paulMccartney.ProfileImage = paulPhoto;
            var paulMember = ArtistMember.Create(beatles, paulMccartney, 2);
            paulMember.Started = DateTime.Parse("2/15/1958");
            paulMember.Ended = DateTime.Parse("8/20/1969");

            _personRepository.AddPerson(_dataSource, paulMccartney);
            _personRepository.AddPersonPhoto(_dataSource, paulMccartney, paulPhoto, 1);
            _artistRepository.AddArtistMember(_dataSource, paulMember);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, rhythmGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, bassGuitar);

            var georgeHarrison = Person.Create("George Harrison");
            georgeHarrison.BornOn = DateTime.Parse("2/25/1943");
            georgeHarrison.DiedOn = DateTime.Parse("11/29/2001");
            georgeHarrison.Twitter = "@GeorgeHarrison";
            georgeHarrison.Bio = "George's bio";
            var georgePhoto = Photo.Create("c:\\images\\profiles\\george-harrison.jpg");
            georgeHarrison.ProfileImage = georgePhoto;
            var georgeMember = ArtistMember.Create(beatles, georgeHarrison, 3);
            georgeMember.Started = DateTime.Parse("2/15/1958");
            georgeMember.Ended = DateTime.Parse("8/20/1969");

            _personRepository.AddPerson(_dataSource, georgeHarrison);
            _personRepository.AddPersonPhoto(_dataSource, georgeHarrison, georgePhoto, 1);
            _artistRepository.AddArtistMember(_dataSource, georgeMember);
            _artistRepository.AddArtistMemberRole(_dataSource, georgeMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, georgeMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, georgeMember, leadGuitar);

            var ringoStarr = Person.Create("Ringo Starr");
            ringoStarr.BornOn = DateTime.Parse("7/7/1940");
            ringoStarr.Twitter = "@ringostarrmusic";
            ringoStarr.Bio = "Ringo's bio";
            var ringoPhoto = Photo.Create("c:\\images\\profiles\\ringo-starr.jpg");
            ringoStarr.ProfileImage = ringoPhoto;
            var ringoMember = ArtistMember.Create(beatles, ringoStarr, 4);
            ringoMember.Started = DateTime.Parse("8/15/1962");
            ringoMember.Ended = DateTime.Parse("8/20/1969");

            _personRepository.AddPerson(_dataSource, ringoStarr);
            _personRepository.AddPersonPhoto(_dataSource, ringoStarr, ringoPhoto, 1);
            _artistRepository.AddArtistMember(_dataSource, ringoMember);
            _artistRepository.AddArtistMemberRole(_dataSource, ringoMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, ringoMember, drums);
        }

        /// <summary>
        /// Tests the get artist method.
        /// </summary>
        [Test]
        public void GetArtistTest()
        {
            var leadGuitar = _roleRepository.GetRole(_dataSource, "Lead Guitar");
            var rhythmGuitar = _roleRepository.GetRole(_dataSource, "Rhythm Guitar");
            var drums = _roleRepository.GetRole(_dataSource, "Drums");

            var artist = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.NotNull(artist);
            Assert.AreEqual(_beatlesId, artist.Id);
            Assert.AreEqual("The Beatles", artist.Name);
            Assert.AreEqual("Beatles bio", artist.Bio);
            Assert.AreEqual(DateTime.Parse("2/15/1958"), artist.StartedOn);
            Assert.AreEqual("@thebeatles", artist.Twitter);
            Assert.AreEqual(DateTime.Parse("8/20/1969"), artist.EndedOn);

            Assert.NotNull(artist.Photos);
            Assert.AreEqual(2, artist.Photos.Count);
            Assert.AreEqual("c:\\images\\beatles-1.jpg", artist.Photos[0].FilePath);
            Assert.AreEqual("c:\\images\\beatles-2.jpg", artist.Photos[1].FilePath);

            Assert.NotNull(artist.Members);
            Assert.AreEqual(4, artist.Members.Count);

            Assert.NotNull(artist.Members[0].Person);
            Assert.AreEqual("John Lennon", artist.Members[0].Person.Name);
            Assert.NotNull(artist.Members[0].Roles);
            Assert.AreEqual(3, artist.Members[0].Roles.Count);
            Assert.IsTrue(artist.Members[0].Roles.Contains(rhythmGuitar));
            Assert.IsFalse(artist.Members[0].Roles.Contains(drums));
            Assert.AreEqual(DateTime.Parse("2/15/1958"), artist.Members[0].Started);
            Assert.AreEqual(DateTime.Parse("8/20/1969"), artist.Members[0].Ended);
            Assert.AreEqual(1, artist.Members[0].Priority);

            Assert.NotNull(artist.Members[1].Person);
            Assert.AreEqual("Paul McCartney", artist.Members[1].Person.Name);
            Assert.NotNull(artist.Members[1].Roles);
            Assert.AreEqual(4, artist.Members[1].Roles.Count);
            Assert.IsTrue(artist.Members[1].Roles.Contains(rhythmGuitar));
            Assert.IsFalse(artist.Members[1].Roles.Contains(drums));
            Assert.AreEqual(DateTime.Parse("2/15/1958"), artist.Members[1].Started);
            Assert.AreEqual(DateTime.Parse("8/20/1969"), artist.Members[1].Ended);
            Assert.AreEqual(2, artist.Members[1].Priority);

            Assert.NotNull(artist.Members[2].Person);
            Assert.AreEqual("George Harrison", artist.Members[2].Person.Name);
            Assert.NotNull(artist.Members[2].Roles);
            Assert.AreEqual(3, artist.Members[2].Roles.Count);
            Assert.IsTrue(artist.Members[2].Roles.Contains(leadGuitar));
            Assert.IsFalse(artist.Members[2].Roles.Contains(drums));
            Assert.AreEqual(DateTime.Parse("2/15/1958"), artist.Members[2].Started);
            Assert.AreEqual(DateTime.Parse("8/20/1969"), artist.Members[2].Ended);
            Assert.AreEqual(3, artist.Members[2].Priority);

            Assert.NotNull(artist.Members[3].Person);
            Assert.AreEqual("Ringo Starr", artist.Members[3].Person.Name);
            Assert.NotNull(artist.Members[3].Roles);
            Assert.AreEqual(2, artist.Members[3].Roles.Count);
            Assert.IsTrue(artist.Members[3].Roles.Contains(drums));
            Assert.IsFalse(artist.Members[3].Roles.Contains(leadGuitar));
            Assert.AreEqual(DateTime.Parse("8/15/1962"), artist.Members[3].Started);
            Assert.AreEqual(DateTime.Parse("8/20/1969"), artist.Members[3].Ended);
            Assert.AreEqual(4, artist.Members[3].Priority);
        }

        /// <summary>
        /// Tests the list artists method.
        /// </summary>
        [Test]
        public void ListArtistsTest()
        {
            var stones = Artist.Create("The Rolling Stones");
            stones.Bio = "Rolling Stones bio";
            stones.Twitter = "@RollingStone";
            stones.StartedOn = DateTime.Parse("6/15/1962");

            _artistRepository.AddArtist(_dataSource, stones);
            _artistRepository.AddArtistPhoto(_dataSource, stones, Photo.Create("c:\\images\\stones.jpg"), 1);

            var bigStar = Artist.Create("Big Star");
            bigStar.Bio = "Big Star bio";
            bigStar.StartedOn = DateTime.Parse("6/1/1971");
            bigStar.EndedOn = DateTime.Parse("3/17/2010");

            _artistRepository.AddArtist(_dataSource, bigStar);
            _artistRepository.AddArtistPhoto(_dataSource, bigStar, Photo.Create("c:\\images\\big-star.jpg"), 1);

            var jimiHendrix = Artist.Create("Jimi Hendrix");
            _artistRepository.AddArtist(_dataSource, jimiHendrix);

            var artists = _artistRepository.ListArtists(_dataSource);

            Assert.NotNull(artists);
            Assert.AreEqual(4, artists.Count);

            Assert.AreEqual("Big Star", artists[0].Name);
            Assert.AreEqual("Big Star bio", artists[0].Bio);
            Assert.AreEqual(DateTime.Parse("6/1/1971"), artists[0].StartedOn);
            Assert.AreEqual(DateTime.Parse("3/17/2010"), artists[0].EndedOn);
            Assert.NotNull(artists[0].ProfileImage);
            Assert.AreEqual("c:\\images\\big-star.jpg", artists[0].ProfileImage.FilePath);

            Assert.AreEqual("Jimi Hendrix", artists[1].Name);
            Assert.IsNull(artists[1].Bio);
            Assert.IsNull(artists[1].StartedOn);
            Assert.IsNull(artists[1].EndedOn);
            Assert.IsNull(artists[1].ProfileImage);

            Assert.AreEqual("The Beatles", artists[2].Name);
            Assert.AreEqual("Beatles bio", artists[2].Bio);
            Assert.AreEqual("@thebeatles", artists[2].Twitter);
            Assert.AreEqual(DateTime.Parse("2/15/1958"), artists[2].StartedOn);
            Assert.AreEqual(DateTime.Parse("8/20/1969"), artists[2].EndedOn);
            Assert.NotNull(artists[2].ProfileImage);
            Assert.AreEqual("c:\\images\\beatles-1.jpg", artists[2].ProfileImage.FilePath);

            Assert.AreEqual("The Rolling Stones", artists[3].Name);
            Assert.AreEqual("Rolling Stones bio", artists[3].Bio);
            Assert.AreEqual("@RollingStone", artists[3].Twitter);
            Assert.AreEqual(DateTime.Parse("6/15/1962"), artists[3].StartedOn);
            Assert.IsNull(artists[3].EndedOn);
            Assert.NotNull(artists[3].ProfileImage);
            Assert.AreEqual("c:\\images\\stones.jpg", artists[3].ProfileImage.FilePath);
        }

        /// <summary>
        /// Tests the delete artist method.
        /// </summary>
        [Test]
        public void DeleteArtistTest()
        {
            var leadVocals = _roleRepository.GetRole(_dataSource, "Lead Vocals");
            var backingVocals = _roleRepository.GetRole(_dataSource, "Backing Vocals");
            var leadGuitar = _roleRepository.GetRole(_dataSource, "Lead Guitar");
            var rhythmGuitar = _roleRepository.GetRole(_dataSource, "Rhythm Guitar");
            var bassGuitar = _roleRepository.GetRole(_dataSource, "Bass Guitar");
            var drums = _roleRepository.GetRole(_dataSource, "Drums");

            var prince = Artist.Create("Prince");
            prince.IsSolo = true;
            prince.StartedOn = DateTime.Parse("6/1/1978");

            _artistRepository.AddArtist(_dataSource, prince);
            _artistRepository.AddArtistPhoto(_dataSource, prince, Photo.Create("c:\\images\\prince.jpg"), 1);

            var princeNelson = Person.Create("Prince Rogers Nelson");
            princeNelson.BornOn = DateTime.Parse("6/7/1958");
            princeNelson.Twitter = "@Prince3EG";
            princeNelson.Bio = "Prince's bio";
            var princePhoto = Photo.Create("c:\\images\\profiles\\prince.jpg");
            princeNelson.ProfileImage = princePhoto;
            var princeMember = ArtistMember.Create(prince, princeNelson, 1);
            princeMember.Started = DateTime.Parse("6/1/1978");

            _artistRepository.AddArtistMember(_dataSource, princeMember);
            _artistRepository.AddArtistMemberRole(_dataSource, princeMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, princeMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, princeMember, leadGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, princeMember, rhythmGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, princeMember, bassGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, princeMember, drums);

            var artist = _artistRepository.GetArtist(_dataSource, prince.Id);
            Assert.NotNull(artist);
            Assert.AreEqual("Prince", artist.Name);
            Assert.IsTrue(artist.IsSolo);
            Assert.AreEqual(1, artist.Members.Count);
            Assert.AreEqual("Prince Rogers Nelson", artist.Members[0].Person.Name);
            Assert.AreEqual(6, artist.Members[0].Roles.Count);

            _artistRepository.DeleteArtist(_dataSource, artist);

            var deletedArtist = _artistRepository.GetArtist(_dataSource, prince.Id);
            Assert.Null(deletedArtist);
        }

        /// <summary>
        /// Tests the delete artist member role method.
        /// </summary>
        [Test]
        public void DeleteArtistMemberRoleTest()
        {
            var beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            var johnMember = beatles.Members[0];
            Assert.AreEqual(3, johnMember.Roles.Count);
            var drums = _roleRepository.GetRole(_dataSource, "Drums");
            Assert.NotNull(drums);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, drums);

            beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            johnMember = beatles.Members[0];
            Assert.AreEqual(4, johnMember.Roles.Count);
            drums = johnMember.Roles.SingleOrDefault(r => r.Name == "Drums");
            Assert.NotNull(drums);
            _artistRepository.DeleteArtistMemberRole(_dataSource, johnMember, drums);

            beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            johnMember = beatles.Members[0];
            Assert.AreEqual(3, johnMember.Roles.Count);
            drums = johnMember.Roles.SingleOrDefault(r => r.Name == "Drums");
            Assert.IsNull(drums);
        }

        /// <summary>
        /// Tests the delete artist member method.
        /// </summary>
        [Test]
        public void DeleteArtistMemberTest()
        {
            var beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            var peteBest = Person.Create("Pete Best");
            _personRepository.AddPerson(_dataSource, peteBest);
            var peteMember = ArtistMember.Create(beatles, peteBest, 5);
            _artistRepository.AddArtistMember(_dataSource, peteMember);
            var drums = _roleRepository.GetRole(_dataSource, "Drums");
            _artistRepository.AddArtistMemberRole(_dataSource, peteMember, drums);

            beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.AreEqual(5, beatles.Members.Count);
            peteMember = beatles.Members[4];
            Assert.AreEqual(peteBest.Id, peteMember.PersonId);
            Assert.NotNull(peteMember.Roles);
            Assert.AreEqual(1, peteMember.Roles.Count);
            Assert.AreEqual("Drums", peteMember.Roles[0].Name);

            _artistRepository.DeleteArtistMember(_dataSource, peteMember);

            beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.AreEqual(4, beatles.Members.Count);
        }

        /// <summary>
        /// Tests the delete artist photo method.
        /// </summary>
        [Test]
        public void DeleteArtistPhotoTest()
        {
            var beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.AreEqual(2, beatles.Photos.Count);

            _artistRepository.AddArtistPhoto(_dataSource, beatles, Photo.Create("c:\\images\\beatles-3.jpg"), 3);
            beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.AreEqual(3, beatles.Photos.Count);
            var photo = beatles.Photos[2];
            Assert.AreEqual("c:\\images\\beatles-3.jpg", photo.FilePath);

            _artistRepository.DeleteArtistPhoto(_dataSource, beatles, photo);

            beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.AreEqual(2, beatles.Photos.Count);
        }
    }
}
