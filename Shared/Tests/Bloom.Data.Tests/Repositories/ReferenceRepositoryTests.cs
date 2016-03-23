using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the reference repository class.
    /// </summary>
    [TestFixture]
    public class ReferenceRepositoryTests
    {
        private const string TestFileName = "ReferenceRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private IPlaylistRepository _playlistRepository;
        private IReferenceRepository _referenceRepository;
        private Guid _referenceId;
        private Guid _unattachedReferenceId;
        private Person _freddyMercury;
        private Artist _queen;
        private Album _aNightAtTheOpera;
        private Song _bohemianRhapsody;
        private Playlist _playlist;
        private Source _life;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            var photoRepository = new PhotoRespository();
            var sourceRepository = new SourceRepository();
            _roleRepository = new RoleRepository();
            _personRepository = new PersonRepository(photoRepository);
            _artistRepository = new ArtistRepository(_roleRepository, photoRepository, _personRepository);
            _songRepository = new SongRepository(_roleRepository, _personRepository);
            _albumRepository = new AlbumRepository(_roleRepository, _personRepository);
            _playlistRepository = new PlaylistRepository(_personRepository);
            _referenceRepository = new ReferenceRepository(sourceRepository);

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
            var time = Source.Create("Time");
            var newYorkTimes = Source.Create("New York Times");
            var rollingStone = Source.Create("Rolling Stone");
            var nme = Source.Create("NME");
            var dailyMail = Source.Create("The Daily Mail");
            var wikipedia = Source.Create("Wikipedia");
            _life = Source.Create("Life");

            var unattachedReference = Reference.Create(_life, "Queen Life Article", "http://archive.life.com/queen");
            _unattachedReferenceId = unattachedReference.Id;
            _referenceRepository.AddReference(_dataSource, unattachedReference);

            _freddyMercury = Person.Create("Freddy Mercury");
            _personRepository.AddPerson(_dataSource, _freddyMercury);
            var freddyReference1 = Reference.Create(wikipedia, "http://www.wikipedia.org/freddy-mercury");
            _referenceRepository.AddReference(_dataSource, freddyReference1);
            _referenceRepository.AddReferenceTo(_dataSource, freddyReference1, _freddyMercury);
            var freddyReference2 = Reference.Create("http://www.random-site/freddy-mercury");
            _referenceRepository.AddReference(_dataSource, freddyReference2);
            _referenceRepository.AddReferenceTo(_dataSource, freddyReference2, _freddyMercury);

            _queen = Artist.Create("Queen");
            _artistRepository.AddArtist(_dataSource, _queen);
            _artistRepository.AddArtistMember(_dataSource, ArtistMember.Create(_queen, _freddyMercury, 1));
            var queenReference1 = Reference.Create(dailyMail, "Queen Daily Mail Article", "http://www.dailymail.com/queen");
            _referenceId = queenReference1.Id;
            _referenceRepository.AddReference(_dataSource, queenReference1);
            _referenceRepository.AddReferenceTo(_dataSource, queenReference1, _queen);
            var queenReference2 = Reference.Create(time, "Queen Time Article", "http://www.time.com/queen");
            _referenceRepository.AddReference(_dataSource, queenReference2);
            _referenceRepository.AddReferenceTo(_dataSource, queenReference2, _queen);
            var queenReference3 = Reference.Create(rollingStone, "Queen Rolling Stone Article", "http://www.rollingstone.com/queen");
            _referenceRepository.AddReference(_dataSource, queenReference3);
            _referenceRepository.AddReferenceTo(_dataSource, queenReference3, _queen);

            _aNightAtTheOpera = Album.Create("A Night at the Opera", _queen);
            _albumRepository.AddAlbum(_dataSource, _aNightAtTheOpera);
            var aNightAtTheOperaReference1 = Reference.Create(newYorkTimes, "A Night at the Opera New York Times Review", "http://nyt.com/album-reviews/a-night-at-the-opera");
            _referenceRepository.AddReference(_dataSource, aNightAtTheOperaReference1);
            _referenceRepository.AddReferenceTo(_dataSource, aNightAtTheOperaReference1, _aNightAtTheOpera);
            var aNightAtTheOperaReference2 = Reference.Create(rollingStone, "A Night at the Opera Rolling Stone Review", "http://www.rollingstone.com/queen/a-night-at-the-opera");
            _referenceRepository.AddReference(_dataSource, aNightAtTheOperaReference2);
            _referenceRepository.AddReferenceTo(_dataSource, aNightAtTheOperaReference2, _aNightAtTheOpera);

            _bohemianRhapsody = Song.Create("Bohemian Rhapsody", _queen);
            _songRepository.AddSong(_dataSource, _bohemianRhapsody);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_aNightAtTheOpera, _bohemianRhapsody, 11));
            var bohemianRhapsodyReference = Reference.Create(nme, "http://www.nme.com/greatest-songs-of-all-time");
            _referenceRepository.AddReference(_dataSource, bohemianRhapsodyReference);
            _referenceRepository.AddReferenceTo(_dataSource, bohemianRhapsodyReference, _bohemianRhapsody);

            _playlist = Playlist.Create("Waynes List", Person.Create("Wayne Campbell"));
            _playlistRepository.AddPlaylist(_dataSource, _playlist);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _bohemianRhapsody, 1));
            _referenceRepository.AddReferenceTo(_dataSource, bohemianRhapsodyReference, _playlist);
            _referenceRepository.AddReferenceTo(_dataSource, aNightAtTheOperaReference1, _playlist);
            _referenceRepository.AddReferenceTo(_dataSource, freddyReference2, _playlist);
        }

        /// <summary>
        /// Tests the reference exists method.
        /// </summary>
        [Test]
        public void ReferenceExistsTest()
        {
            Assert.IsTrue(_referenceRepository.ReferenceExists(_dataSource, _referenceId));
            Assert.IsFalse(_referenceRepository.ReferenceExists(_dataSource, Guid.NewGuid()));
        }

        /// <summary>
        /// Tests the get reference method.
        /// </summary>
        [Test]
        public void GetReferenceTest()
        {
            var reference = _referenceRepository.GetReference(_dataSource, _referenceId);
            Assert.NotNull(reference);
            Assert.AreEqual(_referenceId, reference.Id);
            Assert.AreEqual("http://www.dailymail.com/queen", reference.Url);
            Assert.AreEqual("Queen Daily Mail Article", reference.Title);
            Assert.NotNull(reference.Source);
            Assert.AreEqual("The Daily Mail", reference.Source.Name);
        }

        /// <summary>
        /// Tests the list song references method.
        /// </summary>
        [Test]
        public void ListSongReferences()
        {
            var references = _referenceRepository.ListReferences(_dataSource, _bohemianRhapsody);
            Assert.NotNull(references);
            Assert.AreEqual(1, references.Count);
            Assert.AreEqual("http://www.nme.com/greatest-songs-of-all-time", references[0].Url);
            Assert.NotNull(references[0].Source);
            Assert.AreEqual("NME", references[0].Source.Name);
        }

        /// <summary>
        /// Tests the list album references method.
        /// </summary>
        [Test]
        public void ListAlbumReferencesTest()
        {
            var references = _referenceRepository.ListReferences(_dataSource, _aNightAtTheOpera);
            Assert.NotNull(references);
            Assert.AreEqual(2, references.Count);
            Assert.AreEqual("http://nyt.com/album-reviews/a-night-at-the-opera", references[0].Url);
            Assert.AreEqual("A Night at the Opera New York Times Review", references[0].Title);
            Assert.NotNull(references[0].Source);
            Assert.AreEqual("New York Times", references[0].Source.Name);
            Assert.AreEqual("http://www.rollingstone.com/queen/a-night-at-the-opera", references[1].Url);
            Assert.AreEqual("A Night at the Opera Rolling Stone Review", references[1].Title);
            Assert.NotNull(references[1].Source);
            Assert.AreEqual("Rolling Stone", references[1].Source.Name);
        }

        /// <summary>
        /// Tests the list artist references method.
        /// </summary>
        [Test]
        public void ListArtistReferencesTest()
        {
            var references = _referenceRepository.ListReferences(_dataSource, _queen);
            Assert.NotNull(references);
            Assert.AreEqual(3, references.Count);
            Assert.AreEqual("http://www.dailymail.com/queen", references[0].Url);
            Assert.AreEqual("Queen Daily Mail Article", references[0].Title);
            Assert.NotNull(references[0].Source);
            Assert.AreEqual("The Daily Mail", references[0].Source.Name);
            Assert.AreEqual("http://www.rollingstone.com/queen", references[1].Url);
            Assert.AreEqual("Queen Rolling Stone Article", references[1].Title);
            Assert.NotNull(references[1].Source);
            Assert.AreEqual("Rolling Stone", references[1].Source.Name);
            Assert.AreEqual("http://www.time.com/queen", references[2].Url);
            Assert.AreEqual("Queen Time Article", references[2].Title);
            Assert.NotNull(references[2].Source);
            Assert.AreEqual("Time", references[2].Source.Name);
        }

        /// <summary>
        /// Tests the list person references method.
        /// </summary>
        [Test]
        public void ListPersonReferencesTest()
        {
            var references = _referenceRepository.ListReferences(_dataSource, _freddyMercury);
            Assert.NotNull(references);
            Assert.AreEqual(2, references.Count);
            Assert.AreEqual("http://www.random-site/freddy-mercury", references[0].Url);
            Assert.IsNull(references[0].Source);
            Assert.AreEqual("http://www.wikipedia.org/freddy-mercury", references[1].Url);
            Assert.NotNull(references[1].Source);
            Assert.AreEqual("Wikipedia", references[1].Source.Name);
        }

        /// <summary>
        /// Tests the list playlist references method.
        /// </summary>
        [Test]
        public void ListPlaylistReferencesTest()
        {
            var references = _referenceRepository.ListReferences(_dataSource, _playlist);
            Assert.NotNull(references);
            Assert.AreEqual(3, references.Count);
            Assert.AreEqual("http://nyt.com/album-reviews/a-night-at-the-opera", references[0].Url);
            Assert.AreEqual("A Night at the Opera New York Times Review", references[0].Title);
            Assert.NotNull(references[0].Source);
            Assert.AreEqual("New York Times", references[0].Source.Name);
            Assert.AreEqual("http://www.nme.com/greatest-songs-of-all-time", references[1].Url);
            Assert.NotNull(references[1].Source);
            Assert.AreEqual("NME", references[1].Source.Name);
            Assert.AreEqual("http://www.random-site/freddy-mercury", references[2].Url);
            Assert.IsNull(references[2].Source);
        }

        /// <summary>
        /// Tests the delete reference from song method.
        /// </summary>
        [Test]
        public void DeleteReferenceFromSongTest()
        {
            var unattachedReference = _referenceRepository.GetReference(_dataSource, _unattachedReferenceId);
            Assert.NotNull(unattachedReference);

            var songReferences = _referenceRepository.ListReferences(_dataSource, _bohemianRhapsody);
            Assert.NotNull(songReferences);
            Assert.AreEqual(1, songReferences.Count);

            _referenceRepository.AddReferenceTo(_dataSource, unattachedReference, _bohemianRhapsody);

            songReferences = _referenceRepository.ListReferences(_dataSource, _bohemianRhapsody);
            Assert.AreEqual(2, songReferences.Count);

            _referenceRepository.DeleteReferenceFrom(_dataSource, unattachedReference, _bohemianRhapsody);

            songReferences = _referenceRepository.ListReferences(_dataSource, _bohemianRhapsody);
            Assert.AreEqual(1, songReferences.Count);
        }

        /// <summary>
        /// Tests the delete reference from album method.
        /// </summary>
        [Test]
        public void DeleteReferenceFromAlbumTest()
        {
            var unattachedReference = _referenceRepository.GetReference(_dataSource, _unattachedReferenceId);
            Assert.NotNull(unattachedReference);

            var albumReferences = _referenceRepository.ListReferences(_dataSource, _aNightAtTheOpera);
            Assert.NotNull(albumReferences);
            Assert.AreEqual(2, albumReferences.Count);

            _referenceRepository.AddReferenceTo(_dataSource, unattachedReference, _aNightAtTheOpera);

            albumReferences = _referenceRepository.ListReferences(_dataSource, _aNightAtTheOpera);
            Assert.AreEqual(3, albumReferences.Count);

            _referenceRepository.DeleteReferenceFrom(_dataSource, unattachedReference, _aNightAtTheOpera);

            albumReferences = _referenceRepository.ListReferences(_dataSource, _aNightAtTheOpera);
            Assert.AreEqual(2, albumReferences.Count);
        }

        /// <summary>
        /// Tests the delete reference from artist method.
        /// </summary>
        [Test]
        public void DeleteReferenceFromArtistTest()
        {
            var unattachedReference = _referenceRepository.GetReference(_dataSource, _unattachedReferenceId);
            Assert.NotNull(unattachedReference);

            var artistReferences = _referenceRepository.ListReferences(_dataSource, _queen);
            Assert.NotNull(artistReferences);
            Assert.AreEqual(3, artistReferences.Count);

            _referenceRepository.AddReferenceTo(_dataSource, unattachedReference, _queen);

            artistReferences = _referenceRepository.ListReferences(_dataSource, _queen);
            Assert.AreEqual(4, artistReferences.Count);

            _referenceRepository.DeleteReferenceFrom(_dataSource, unattachedReference, _queen);

            artistReferences = _referenceRepository.ListReferences(_dataSource, _queen);
            Assert.AreEqual(3, artistReferences.Count);
        }

        /// <summary>
        /// Tests the delete reference from person method.
        /// </summary>
        [Test]
        public void DeleteReferenceFromPersonTest()
        {
            var unattachedReference = _referenceRepository.GetReference(_dataSource, _unattachedReferenceId);
            Assert.NotNull(unattachedReference);

            var personReferences = _referenceRepository.ListReferences(_dataSource, _freddyMercury);
            Assert.NotNull(personReferences);
            Assert.AreEqual(2, personReferences.Count);

            _referenceRepository.AddReferenceTo(_dataSource, unattachedReference, _freddyMercury);

            personReferences = _referenceRepository.ListReferences(_dataSource, _freddyMercury);
            Assert.AreEqual(3, personReferences.Count);

            _referenceRepository.DeleteReferenceFrom(_dataSource, unattachedReference, _freddyMercury);

            personReferences = _referenceRepository.ListReferences(_dataSource, _freddyMercury);
            Assert.AreEqual(2, personReferences.Count);
        }

        /// <summary>
        /// Tests the delete reference from playlist method.
        /// </summary>
        [Test]
        public void DeleteReferenceFromPlaylistTest()
        {
            var unattachedReference = _referenceRepository.GetReference(_dataSource, _unattachedReferenceId);
            Assert.NotNull(unattachedReference);

            var playlistReferences = _referenceRepository.ListReferences(_dataSource, _playlist);
            Assert.NotNull(playlistReferences);
            Assert.AreEqual(3, playlistReferences.Count);

            _referenceRepository.AddReferenceTo(_dataSource, unattachedReference, _playlist);

            playlistReferences = _referenceRepository.ListReferences(_dataSource, _playlist);
            Assert.AreEqual(4, playlistReferences.Count);

            _referenceRepository.DeleteReferenceFrom(_dataSource, unattachedReference, _playlist);

            playlistReferences = _referenceRepository.ListReferences(_dataSource, _playlist);
            Assert.AreEqual(3, playlistReferences.Count);
        }

        /// <summary>
        /// Tests the delete reference method.
        /// </summary>
        [Test]
        public void DeleteReferenceTest()
        {
            var gutterBubbles = Source.Create("Gutterbubbles");
            var reference = Reference.Create(gutterBubbles, "A Queen Retrospective", "http://www.gutterbubbles.com/a-queen-retrospective");
            _referenceRepository.AddReference(_dataSource, reference);
            _referenceRepository.AddReferenceTo(_dataSource, reference, _freddyMercury);
            _referenceRepository.AddReferenceTo(_dataSource, reference, _queen);
            _referenceRepository.AddReferenceTo(_dataSource, reference, _aNightAtTheOpera);
            _referenceRepository.AddReferenceTo(_dataSource, reference, _bohemianRhapsody);
            _referenceRepository.AddReferenceTo(_dataSource, reference, _playlist);

            reference = _referenceRepository.GetReference(_dataSource, reference.Id);
            Assert.NotNull(reference);

            _referenceRepository.DeleteReference(_dataSource, reference);

            var deletedReference = _referenceRepository.GetReference(_dataSource, reference.Id);
            Assert.IsNull(deletedReference);
        }
    }
}
