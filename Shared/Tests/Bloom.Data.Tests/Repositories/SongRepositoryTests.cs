using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the song repository class.
    /// </summary>
    [TestFixture]
    public class SongRepositoryTests
    {
        private const string TestFileName = "SongRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IGenreRepository _genreRepository;
        private ITimeSignatureRepository _timeSignatureRepository;
        private Guid _atomHeartMotherId;
        private Guid _pinkFloydId;
        private Guid _classicRockId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            var photoRepository = new PhotoRespository();
            var roleRepository = new RoleRepository();
            var personRepository = new PersonRepository(photoRepository);
            _artistRepository = new ArtistRepository(roleRepository, photoRepository, personRepository);
            _genreRepository = new GenreRepository();
            _timeSignatureRepository = new TimeSignatureRepository();
            _songRepository = new SongRepository();

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
            var pinkFloyd = Artist.Create("Pink Floyd");
            _pinkFloydId = pinkFloyd.Id;
            _artistRepository.AddArtist(_dataSource, pinkFloyd);

            var classicRock = Genre.Create("Classic Rock");
            _classicRockId = classicRock.Id;
            _genreRepository.AddGenre(_dataSource, classicRock);

            var fourFour = TimeSignature.Create(4, BeatLength.Quarter);
            _timeSignatureRepository.AddTimeSignature(_dataSource, fourFour);
            var sixFour = TimeSignature.Create(6, BeatLength.Quarter);
            _timeSignatureRepository.AddTimeSignature(_dataSource, sixFour);

            var atomHeartMother = Song.Create("Atom Heart Mother Suite", pinkFloyd);
            _atomHeartMotherId = atomHeartMother.Id;
            atomHeartMother.Genre = classicRock;
            atomHeartMother.TimeSignature = fourFour;
            atomHeartMother.Bpm = 69;
            atomHeartMother.Length = 1424000;
            atomHeartMother.Key = MusicalKeys.C;
            atomHeartMother.Description = "Atom Heart Mother Suite description";
            atomHeartMother.Lyrics = "Silence in the studio!";
            _songRepository.AddSong(_dataSource, atomHeartMother);
        }

        /// <summary>
        /// Tests the get song method.
        /// </summary>
        [Test]
        public void GetSongTests()
        {
            var atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
        }
    }
}
