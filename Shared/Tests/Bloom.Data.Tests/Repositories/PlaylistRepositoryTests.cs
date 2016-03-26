using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the playlist repository class.
    /// </summary>
    [TestFixture]
    public class PlaylistRepositoryTests
    {
        private const string TestFileName = "PlaylistRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private IPlaylistRepository _playlistRepository;
        private Song _flowersOnTheWall;
        private Guid _playlist1Id;
        private Guid _playlist2Id;
        private Guid _playlist3Id;
        private Guid _janeDoeId;
        private Guid _atomHeartMotherSuiteId;
        private Guid _aDayInTheLifeId;
        private Guid _alansPsychedelicBreakfastId;

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
            _personRepository = new PersonRepository(photoRepository);
            _artistRepository = new ArtistRepository(roleRepository, photoRepository, _personRepository);
            _songRepository = new SongRepository(roleRepository, _personRepository);
            _albumRepository = new AlbumRepository(roleRepository, _personRepository);
            _playlistRepository = new PlaylistRepository(_personRepository);

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
            _artistRepository.AddArtist(_dataSource, pinkFloyd);

            var atomHeartMother = Album.Create("Atom Heart Mother", pinkFloyd);
            _albumRepository.AddAlbum(_dataSource, atomHeartMother);
            var atomHeartMotherSuite = Song.Create("Atom Heart Mother Suite", pinkFloyd);
            _atomHeartMotherSuiteId = atomHeartMotherSuite.Id;
            _songRepository.AddSong(_dataSource, atomHeartMotherSuite);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(atomHeartMother, atomHeartMotherSuite, 1, 'A'));
            var ifSong = Song.Create("If", pinkFloyd);
            _songRepository.AddSong(_dataSource, ifSong);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(atomHeartMother, ifSong, 2, 'B'));
            var summer68 = Song.Create("Summer '68", pinkFloyd);
            _songRepository.AddSong(_dataSource, summer68);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(atomHeartMother, summer68, 3, 'B'));
            var fatOldSun = Song.Create("Fat Old Sun", pinkFloyd);
            _songRepository.AddSong(_dataSource, fatOldSun);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(atomHeartMother, fatOldSun, 4, 'B'));
            var alansPsychedelicBreakfast = Song.Create("Alan's Psychedelic Breakfast", pinkFloyd);
            _alansPsychedelicBreakfastId = alansPsychedelicBreakfast.Id;
            _songRepository.AddSong(_dataSource, alansPsychedelicBreakfast);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(atomHeartMother, alansPsychedelicBreakfast, 5, 'B'));

            var beatles = Artist.Create("The Beatles");
            _artistRepository.AddArtist(_dataSource, beatles);

            var sgtPepper = Album.Create("Sgt. Pepper's Lonely Hearts Club Band", beatles);
            _albumRepository.AddAlbum(_dataSource, sgtPepper);
            var sgtPepperSong = Song.Create("Sgt. Pepper's Lonely Hearts Club Band", beatles);
            _songRepository.AddSong(_dataSource, sgtPepperSong);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, sgtPepperSong, 1, 'A'));
            var helpFromMyFriends = Song.Create("With a Little Help from My Friends", beatles);
            _songRepository.AddSong(_dataSource, helpFromMyFriends);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, helpFromMyFriends, 2, 'A'));
            var lucyInTheSky = Song.Create("Lucy in the Sky with Diamonds", beatles);
            _songRepository.AddSong(_dataSource, lucyInTheSky);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, lucyInTheSky, 3, 'A'));
            var gettingBetter = Song.Create("Getting Better", beatles);
            _songRepository.AddSong(_dataSource, gettingBetter);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, gettingBetter, 4, 'A'));
            var fixingAHole = Song.Create("Fixing a Hole", beatles);
            _songRepository.AddSong(_dataSource, fixingAHole);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, fixingAHole, 5, 'A'));
            var shesLeavingHome = Song.Create("She's Leaving Home", beatles);
            _songRepository.AddSong(_dataSource, shesLeavingHome);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, shesLeavingHome, 6, 'A'));
            var mrKite = Song.Create("Being for the Benefit of Mr. Kite!", beatles);
            _songRepository.AddSong(_dataSource, mrKite);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, mrKite, 7, 'A'));
            var withinYouWithoutYou = Song.Create("Within You Without You", beatles);
            _songRepository.AddSong(_dataSource, withinYouWithoutYou);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, withinYouWithoutYou, 8, 'B'));
            var whenIm64 = Song.Create("When I'm Sixty-Four", beatles);
            _songRepository.AddSong(_dataSource, whenIm64);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, whenIm64, 9, 'B'));
            var lovelyRita = Song.Create("Lovely Rita", beatles);
            _songRepository.AddSong(_dataSource, lovelyRita);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, lovelyRita, 10, 'B'));
            var goodMorningGoodMorning = Song.Create("Good Morning Good Morning", beatles);
            _songRepository.AddSong(_dataSource, goodMorningGoodMorning);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, goodMorningGoodMorning, 11, 'B'));
            var sgtPepperReprise = Song.Create("Sgt. Pepper's Lonely Hearts Club Band (Reprise)", beatles);
            _songRepository.AddSong(_dataSource, sgtPepperReprise);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, sgtPepperReprise, 12, 'B'));
            var aDayInTheLife = Song.Create("A Day In the Life", beatles);
            _aDayInTheLifeId = aDayInTheLife.Id;
            _songRepository.AddSong(_dataSource, aDayInTheLife);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, aDayInTheLife, 13, 'B'));

            var whiteAlbum = Album.Create("The Beatles", beatles);
            _albumRepository.AddAlbum(_dataSource, whiteAlbum);
            whiteAlbum.UnofficialName = "The White Album";
            var backInTheUssr = Song.Create("Back in the USSR", beatles);
            _songRepository.AddSong(_dataSource, backInTheUssr);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(whiteAlbum, backInTheUssr, 1, 1, '1'));
            var marthaMyDear = Song.Create("Martha My Dear", beatles);
            _songRepository.AddSong(_dataSource, marthaMyDear);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(whiteAlbum, marthaMyDear, 9, 1, '2'));
            var birthday = Song.Create("Birthday", beatles);
            _songRepository.AddSong(_dataSource, birthday);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(whiteAlbum, birthday, 1, 2, '3'));
            var revolution1 = Song.Create("Revolution 1", beatles);
            _songRepository.AddSong(_dataSource, revolution1);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(whiteAlbum, revolution1, 8, 2, '4'));

            var pulpFictionSoundtrack = Album.Create("Pulp Fiction Original Soundtrack");
            pulpFictionSoundtrack.IsSoundtrack = true;
            pulpFictionSoundtrack.IsMixedArtist = true;
            pulpFictionSoundtrack.IsCompilation = true;
            _albumRepository.AddAlbum(_dataSource, pulpFictionSoundtrack);
            var dickDale = Artist.Create("Dick Dale & His Del-Tones");
            _artistRepository.AddArtist(_dataSource, dickDale);
            var misirlou = Song.Create("Misirlou", dickDale);
            _songRepository.AddSong(_dataSource, misirlou);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, misirlou, 1));
            var johnAndSam = Artist.Create("John Travolta and Samuel L. Jackson");
            _artistRepository.AddArtist(_dataSource, johnAndSam);
            var royaleWithCheese = Song.Create("Royale with Cheese", johnAndSam);
            _songRepository.AddSong(_dataSource, royaleWithCheese);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, royaleWithCheese, 2));
            var koolAndTheGang = Artist.Create("Kool & The Gang");
            _artistRepository.AddArtist(_dataSource, koolAndTheGang);
            var jungleBoogie = Song.Create("Jungle Boogie", koolAndTheGang);
            _songRepository.AddSong(_dataSource, jungleBoogie);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, jungleBoogie, 3));
            var alGreen = Artist.Create("Al Green");
            _artistRepository.AddArtist(_dataSource, alGreen);
            var letsStayTogether = Song.Create("Let's Stay Together", alGreen);
            _songRepository.AddSong(_dataSource, letsStayTogether);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, letsStayTogether, 4));
            var tornados = Artist.Create("The Tornados");
            _artistRepository.AddArtist(_dataSource, tornados);
            var bustinSurfboards = Song.Create("Bustin' Surfboards", tornados);
            _songRepository.AddSong(_dataSource, bustinSurfboards);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, bustinSurfboards, 5));
            var rickyNelson = Artist.Create("Ricky Nelson");
            _artistRepository.AddArtist(_dataSource, rickyNelson);
            var lonesomeTown = Song.Create("Lonesome Town", rickyNelson);
            _songRepository.AddSong(_dataSource, lonesomeTown);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, lonesomeTown, 6));
            var dustySpringfield = Artist.Create("Dusty Springfield");
            _artistRepository.AddArtist(_dataSource, dustySpringfield);
            var sonOfAPreacherMan = Song.Create("Son of a Preacher Man", dustySpringfield);
            _songRepository.AddSong(_dataSource, sonOfAPreacherMan);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, sonOfAPreacherMan, 7));
            var centurians = Artist.Create("The Centurians");
            _artistRepository.AddArtist(_dataSource, centurians);
            var bullwinkle2 = Song.Create("Bullwinkle Part II", centurians);
            _songRepository.AddSong(_dataSource, bullwinkle2);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, bullwinkle2, 8));
            var chuckBerry = Artist.Create("Chuck Berry");
            _artistRepository.AddArtist(_dataSource, chuckBerry);
            var youNeverCanTell = Song.Create("You Never Can Tell", chuckBerry);
            _songRepository.AddSong(_dataSource, youNeverCanTell);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, youNeverCanTell, 9));
            var neilDiamond = Artist.Create("Neil Diamond");
            _artistRepository.AddArtist(_dataSource, neilDiamond);
            var girlYoullBeAWomanSoon = Song.Create("Girl, You'll Be a Woman Soon", neilDiamond);
            _songRepository.AddSong(_dataSource, girlYoullBeAWomanSoon);
            var urgeOverkill = Artist.Create("Urge Overkill");
            _artistRepository.AddArtist(_dataSource, urgeOverkill);
            var girlYoullBeAWomanSoonCover = Song.Create("Girl, You'll Be A Woman Soon", urgeOverkill);
            girlYoullBeAWomanSoonCover.IsCover = true;
            girlYoullBeAWomanSoonCover.OriginalSongId = girlYoullBeAWomanSoon.Id;
            _songRepository.AddSong(_dataSource, girlYoullBeAWomanSoonCover);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, girlYoullBeAWomanSoonCover, 10));
            var mariaMcKee = Artist.Create("Maria McKee");
            _artistRepository.AddArtist(_dataSource, mariaMcKee);
            var ifLoveIsARedDress = Song.Create("If Love Is A Red Dress (Hang Me In Rags)", mariaMcKee);
            _songRepository.AddSong(_dataSource, ifLoveIsARedDress);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, ifLoveIsARedDress, 11));
            var revels = Artist.Create("The Revels");
            _artistRepository.AddArtist(_dataSource, revels);
            var comanche = Song.Create("Comanche", revels);
            _songRepository.AddSong(_dataSource, comanche);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, comanche, 12));
            var statlerBrothers = Artist.Create("The Statler Brothers");
            _artistRepository.AddArtist(_dataSource, statlerBrothers);
            _flowersOnTheWall = Song.Create("Flowers on the Wall", statlerBrothers);
            _songRepository.AddSong(_dataSource, _flowersOnTheWall);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, _flowersOnTheWall, 13));
            var personality = Song.Create("Personality Goes a Long Way", johnAndSam);
            _songRepository.AddSong(_dataSource, personality);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, personality, 14));
            var livelyOnes = Artist.Create("The Lively Ones");
            _artistRepository.AddArtist(_dataSource, livelyOnes);
            var surfRider = Song.Create("Surf Rider", livelyOnes);
            _songRepository.AddSong(_dataSource, surfRider);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, surfRider, 15));
            var samJackson = Artist.Create("Samuel L. Jackson");
            _artistRepository.AddArtist(_dataSource, samJackson);
            var ezekiel2517 = Song.Create("Ezekiel 25:17", samJackson);
            _songRepository.AddSong(_dataSource, ezekiel2517);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, ezekiel2517, 16));

            var johnDoe = Person.Create("John Doe");
            var janeDoe = Person.Create("Jane Doe");
            _janeDoeId = janeDoe.Id;
            var playlist1 = Playlist.Create("Test Playlist 1", johnDoe);
            _playlist1Id = playlist1.Id;
            playlist1.Description = "Test Playlist Description 1";
            playlist1.Length = 500000;
            playlist1.Rating = 4;
            _playlistRepository.AddPlaylist(_dataSource, playlist1);
            _playlistRepository.AddPlaylistArtwork(_dataSource, PlaylistArtwork.Create(playlist1, "c:\\images\\playlist-1.jpg", 1));
            _playlistRepository.AddPlaylistArtwork(_dataSource, PlaylistArtwork.Create(playlist1, "c:\\images\\playlist-2.jpg", 2));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist1, ezekiel2517, 1));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist1, misirlou, 2));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist1, backInTheUssr, 3));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist1, helpFromMyFriends, 4));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist1, fatOldSun, 5));

            var playlist2 = Playlist.Create("Test Playlist 2", janeDoe);
            _playlist2Id = playlist2.Id;
            _playlistRepository.AddPlaylist(_dataSource, playlist2);

            var playlist3 = Playlist.Create("Test Playlist 3", johnDoe);
            _playlist3Id = playlist3.Id;
            _playlistRepository.AddPlaylist(_dataSource, playlist3);
        }

        /// <summary>
        /// Tests the get playlist method.
        /// </summary>
        [Test]
        public void GetPlaylistTests()
        {
            var playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.NotNull(playlist);
            Assert.AreEqual(_playlist1Id, playlist.Id);
            Assert.AreEqual("Test Playlist 1", playlist.Name);
            Assert.AreEqual("Test Playlist Description 1", playlist.Description);
            Assert.AreEqual(500000, playlist.Length);
            Assert.AreEqual(4, playlist.Rating);
            Assert.LessOrEqual(playlist.CreatedOn, DateTime.Now);
            Assert.Greater(playlist.CreatedOn, DateTime.Now.AddMinutes(-1));
            Assert.NotNull(playlist.CreatedBy);
            Assert.AreEqual("John Doe", playlist.CreatedBy.Name);
            Assert.NotNull(playlist.Artwork);
            Assert.AreEqual(2, playlist.Artwork.Count);
            Assert.AreEqual("c:\\images\\playlist-1.jpg", playlist.Artwork[0].FilePath);
            Assert.AreEqual("c:\\images\\playlist-2.jpg", playlist.Artwork[1].FilePath);
            Assert.NotNull(playlist.Tracks);
            Assert.AreEqual(5, playlist.Tracks.Count);
            Assert.AreEqual(1, playlist.Tracks[0].TrackNumber);
            Assert.AreEqual("Ezekiel 25:17", playlist.Tracks[0].Song.Name);
            Assert.AreEqual("Samuel L. Jackson", playlist.Tracks[0].Song.Artist.Name);
            Assert.AreEqual(2, playlist.Tracks[1].TrackNumber);
            Assert.AreEqual("Misirlou", playlist.Tracks[1].Song.Name);
            Assert.AreEqual("Dick Dale & His Del-Tones", playlist.Tracks[1].Song.Artist.Name);
            Assert.AreEqual(3, playlist.Tracks[2].TrackNumber);
            Assert.AreEqual("Back in the USSR", playlist.Tracks[2].Song.Name);
            Assert.AreEqual("The Beatles", playlist.Tracks[2].Song.Artist.Name);
            Assert.AreEqual(4, playlist.Tracks[3].TrackNumber);
            Assert.AreEqual("With a Little Help from My Friends", playlist.Tracks[3].Song.Name);
            Assert.AreEqual("The Beatles", playlist.Tracks[3].Song.Artist.Name);
            Assert.AreEqual(5, playlist.Tracks[4].TrackNumber);
            Assert.AreEqual("Fat Old Sun", playlist.Tracks[4].Song.Name);
            Assert.AreEqual("Pink Floyd", playlist.Tracks[4].Song.Artist.Name);
        }

        /// <summary>
        /// Tests the list playlists method.
        /// </summary>
        [Test]
        public void ListPlaylistsTest()
        {
            var playlists = _playlistRepository.ListPlaylists(_dataSource);
            Assert.NotNull(playlists);
            Assert.AreEqual(3, playlists.Count);
            Assert.AreEqual(_playlist1Id, playlists[2].Id);
            Assert.AreEqual("Test Playlist 1", playlists[2].Name);
            Assert.AreEqual("Test Playlist Description 1", playlists[2].Description);
            Assert.AreEqual(500000, playlists[2].Length);
            Assert.AreEqual(4, playlists[2].Rating);
            Assert.LessOrEqual(playlists[2].CreatedOn, DateTime.Now);
            Assert.Greater(playlists[2].CreatedOn, DateTime.Now.AddMinutes(-1));
            Assert.NotNull(playlists[2].CreatedBy);
            Assert.AreEqual("John Doe", playlists[2].CreatedBy.Name);
            Assert.AreEqual(_playlist2Id, playlists[1].Id);
            Assert.AreEqual("Test Playlist 2", playlists[1].Name);
            Assert.NotNull(playlists[1].CreatedBy);
            Assert.AreEqual("Jane Doe", playlists[1].CreatedBy.Name);
            Assert.AreEqual(_playlist3Id, playlists[0].Id);
            Assert.AreEqual("Test Playlist 3", playlists[0].Name);
            Assert.NotNull(playlists[0].CreatedBy);
            Assert.AreEqual("John Doe", playlists[0].CreatedBy.Name);
        }

        /// <summary>
        /// Tests the delete playlist track method.
        /// </summary>
        [Test]
        public void DeletePlaylistTrackTest()
        {
            var playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.NotNull(playlist);
            Assert.NotNull(playlist.Tracks);
            Assert.AreEqual(5, playlist.Tracks.Count);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist, _flowersOnTheWall, 6));

            playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.AreEqual(6, playlist.Tracks.Count);
            Assert.AreEqual(_flowersOnTheWall.Name, playlist.Tracks[5].Song.Name);
            _playlistRepository.DeletePlaylistTrack(_dataSource, playlist.Tracks[5]);

            playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.AreEqual(5, playlist.Tracks.Count);
        }

        /// <summary>
        /// Tests the delete playlist artwork method.
        /// </summary>
        [Test]
        public void DeletePlaylistArtworkTest()
        {
            var playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.NotNull(playlist.Artwork);
            Assert.AreEqual(2, playlist.Artwork.Count);

            _playlistRepository.AddPlaylistArtwork(_dataSource, PlaylistArtwork.Create(playlist, "c:\\images\\playlist-3.jpg", 3));

            playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.AreEqual(3, playlist.Artwork.Count);
            Assert.AreEqual("c:\\images\\playlist-3.jpg", playlist.Artwork[2].FilePath);

            _playlistRepository.DeletePlaylistArtwork(_dataSource, playlist.Artwork[2]);

            playlist = _playlistRepository.GetPlaylist(_dataSource, _playlist1Id);
            Assert.AreEqual(2, playlist.Artwork.Count);
        }

        /// <summary>
        /// Tests the delete playlist method.
        /// </summary>
        [Test]
        public void DeletePlaylistTest()
        {
            var janeDoe = _personRepository.GetPerson(_dataSource, _janeDoeId);
            Assert.NotNull(janeDoe);
            var playlist4 = Playlist.Create("Test Playlist 4", janeDoe);
            playlist4.Description = "Test Playlist Description 4";
            playlist4.Length = 700000;
            _playlistRepository.AddPlaylist(_dataSource, playlist4);
            _playlistRepository.AddPlaylistArtwork(_dataSource, PlaylistArtwork.Create(playlist4, "c:\\images\\playlist4-1.jpg", 1));
            _playlistRepository.AddPlaylistArtwork(_dataSource, PlaylistArtwork.Create(playlist4, "c:\\images\\playlist4-2.jpg", 2));
            var atomHeartMotherSuite = _songRepository.GetSong(_dataSource, _atomHeartMotherSuiteId);
            Assert.NotNull(atomHeartMotherSuite);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist4, atomHeartMotherSuite, 1));
            var aDayInTheLife = _songRepository.GetSong(_dataSource, _aDayInTheLifeId);
            Assert.NotNull(aDayInTheLife);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist4, aDayInTheLife, 2));
            var alansPsychedelicBreakfast = _songRepository.GetSong(_dataSource, _alansPsychedelicBreakfastId);
            Assert.NotNull(alansPsychedelicBreakfast);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(playlist4, alansPsychedelicBreakfast, 3));

            playlist4 = _playlistRepository.GetPlaylist(_dataSource, playlist4.Id);
            Assert.NotNull(playlist4);
            Assert.NotNull(playlist4.Artwork);
            Assert.AreEqual(2, playlist4.Artwork.Count);
            Assert.NotNull(playlist4.Tracks);
            Assert.AreEqual(3, playlist4.Tracks.Count);

            _playlistRepository.DeletePlaylist(_dataSource, playlist4);

            playlist4 = _playlistRepository.GetPlaylist(_dataSource, playlist4.Id);
            Assert.IsNull(playlist4);
        }
    }
}
