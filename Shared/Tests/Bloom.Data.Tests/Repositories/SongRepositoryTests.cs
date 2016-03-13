using System;
using System.IO;
using System.Linq;
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
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private ITimeSignatureRepository _timeSignatureRepository;
        private IHolidayRepository _holidayRepository;
        private Guid _christmasId;
        private Guid _classicRockId;
        private Guid _pinkFloydId;
        private Guid _atomHeartMotherId;
        private Guid _fatOldSunId;
        private Guid _setTheControlsId;
        private Guid _setTheControlsLiveId;
        private Guid _lucyInTheSkyId;
        private Guid _lucyInTheSkyCoverId;
        private Guid _happyXmasId;
        private Guid _fridayImInLoveId;
        private Guid _fridayImInLoveRemixId;
        private Guid _creepId;
        private Guid _sessionId;

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
            _genreRepository = new GenreRepository();
            _timeSignatureRepository = new TimeSignatureRepository();
            _holidayRepository = new HolidayRepository();
            _songRepository = new SongRepository(_roleRepository, _personRepository);

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

            var bassGuitar = Role.Create("Bass Guitar");
            _roleRepository.AddRole(_dataSource, bassGuitar);
            var leadGuitar = Role.Create("Lead Guitar");
            _roleRepository.AddRole(_dataSource, leadGuitar);
            var rhythmGuitar = Role.Create("Rhythm Guitar");
            _roleRepository.AddRole(_dataSource, rhythmGuitar);
            var keyboards = Role.Create("Keyboards");
            _roleRepository.AddRole(_dataSource, keyboards);
            var drums = Role.Create("Drums");
            _roleRepository.AddRole(_dataSource, drums);
            var producer = Role.Create("Producer");
            _roleRepository.AddRole(_dataSource, producer);

            var rogerWaters = Person.Create("Roger Waters");
            _personRepository.AddPerson(_dataSource, rogerWaters);
            var rogerMember = ArtistMember.Create(pinkFloyd, rogerWaters, 1);
            _artistRepository.AddArtistMember(_dataSource, rogerMember);
            _artistRepository.AddArtistMemberRole(_dataSource, rogerMember, bassGuitar);
            var davidGilmour = Person.Create("David Gilmour");
            _personRepository.AddPerson(_dataSource, davidGilmour);
            var davidMember = ArtistMember.Create(pinkFloyd, davidGilmour, 2);
            _artistRepository.AddArtistMember(_dataSource, davidMember);
            _artistRepository.AddArtistMemberRole(_dataSource, davidMember, leadGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, davidMember, rhythmGuitar);
            var nickMason = Person.Create("Nick Mason");
            _personRepository.AddPerson(_dataSource, nickMason);
            var nickMember = ArtistMember.Create(pinkFloyd, nickMason, 3);
            _artistRepository.AddArtistMember(_dataSource, nickMember);
            _artistRepository.AddArtistMemberRole(_dataSource, nickMember, drums);
            var richardWright = Person.Create("Richard Wright");
            _personRepository.AddPerson(_dataSource, richardWright);
            var richardMember = ArtistMember.Create(pinkFloyd, richardWright, 4);
            _artistRepository.AddArtistMember(_dataSource, richardMember);
            _artistRepository.AddArtistMemberRole(_dataSource, richardMember, keyboards);
            var normanSmith = Person.Create("Norman Smith");
            _personRepository.AddPerson(_dataSource, normanSmith);

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

            var segment1 = SongSegment.Create(atomHeartMother, 0, 170000, "Father's Shout");
            segment1.Bpm = 80;
            _songRepository.AddSongSegment(_dataSource, segment1);
            var segment2 = SongSegment.Create(atomHeartMother, 170000, 323000, "Breast Milky");
            segment2.TimeSignature = sixFour;
            segment2.Key = MusicalKeys.G;
            _songRepository.AddSongSegment(_dataSource, segment2);

            var rogerCredit = SongCredit.Create(atomHeartMother, rogerWaters);
            _songRepository.AddSongCredit(_dataSource, rogerCredit);
            _songRepository.AddSongCreditRole(_dataSource, rogerCredit, bassGuitar);
            var davidCredit = SongCredit.Create(atomHeartMother, davidGilmour);
            _songRepository.AddSongCredit(_dataSource, davidCredit);
            _songRepository.AddSongCreditRole(_dataSource, davidCredit, rhythmGuitar);
            _songRepository.AddSongCreditRole(_dataSource, davidCredit, leadGuitar);
            var nickCredit = SongCredit.Create(atomHeartMother, nickMason);
            _songRepository.AddSongCredit(_dataSource, nickCredit);
            _songRepository.AddSongCreditRole(_dataSource, nickCredit, drums);
            var richardCredit = SongCredit.Create(atomHeartMother, richardWright);
            _songRepository.AddSongCredit(_dataSource, richardCredit);
            _songRepository.AddSongCreditRole(_dataSource, richardCredit, keyboards);
            var normanCredit = SongCredit.Create(atomHeartMother, normanSmith);
            _songRepository.AddSongCredit(_dataSource, normanCredit);
            _songRepository.AddSongCreditRole(_dataSource, normanCredit, producer);

            var session1 = RecordingSession.Create(atomHeartMother, DateTime.Parse("2/15/1970"));
            _sessionId = session1.Id;
            session1.Notes = "Session One Notes";
            _songRepository.AddRecordingSession(_dataSource, session1);
            var session2 = RecordingSession.Create(atomHeartMother, DateTime.Parse("2/20/1970"));
            session2.Notes = "Session Two Notes";
            _songRepository.AddRecordingSession(_dataSource, session2);
            var session3 = RecordingSession.Create(atomHeartMother, DateTime.Parse("3/1/1970"));
            session3.Notes = "Session Three Notes";
            _songRepository.AddRecordingSession(_dataSource, session3);

            var fatOldSun = Song.Create("Fat Old Sun", pinkFloyd);
            _fatOldSunId = fatOldSun.Id;
            _songRepository.AddSong(_dataSource, fatOldSun);

            var setTheControls = Song.Create("Set the Controls for the Heart of the Sun", pinkFloyd);
            _setTheControlsId = setTheControls.Id;
            setTheControls.Genre = classicRock;
            setTheControls.BestPlayedAtStart = 300000;
            setTheControls.BestPlayedAtStop = 600000;
            _songRepository.AddSong(_dataSource, setTheControls);
            var setTheControlsLive = Song.Create("Set the Controls for the Heart of the Sun", pinkFloyd);
            _setTheControlsLiveId = setTheControlsLive.Id;
            setTheControlsLive.Genre = classicRock;
            setTheControlsLive.IsLive = true;
            setTheControlsLive.OriginalSongId = _setTheControlsId;
            _songRepository.AddSong(_dataSource, setTheControlsLive);

            var beatles = Artist.Create("The Beatles");
            _artistRepository.AddArtist(_dataSource, beatles);
            var flamingLips = Artist.Create("The Flaming Lips");
            _artistRepository.AddArtist(_dataSource, flamingLips);
            var moby = Artist.Create("Moby");
            _artistRepository.AddArtist(_dataSource, moby);
            var mileyCyrus = Artist.Create("Miley Cyrus");
            _artistRepository.AddArtist(_dataSource, mileyCyrus);

            var lucyInTheSky = Song.Create("Lucy in the Sky with Diamonds", beatles);
            _lucyInTheSkyId = lucyInTheSky.Id;
            _songRepository.AddSong(_dataSource, lucyInTheSky);
            var lucyInTheSkyCover = Song.Create("Lucy in the Sky with Diamonds", flamingLips);
            _lucyInTheSkyCoverId = lucyInTheSkyCover.Id;
            lucyInTheSkyCover.IsCover = true;
            lucyInTheSkyCover.OriginalSongId = _lucyInTheSkyId;
            _songRepository.AddSong(_dataSource, lucyInTheSkyCover);

            var mileyCollaborator = SongCollaborator.Create(lucyInTheSkyCover, mileyCyrus);
            mileyCollaborator.IsFeatured = true;
            _songRepository.AddSongCollaborator(_dataSource, mileyCollaborator);

            var mobyCollaborator = SongCollaborator.Create(lucyInTheSkyCover, moby);
            mobyCollaborator.IsFeatured = false;
            _songRepository.AddSongCollaborator(_dataSource, mobyCollaborator);

            var christmas = Holiday.Create("Christmas");
            _christmasId = christmas.Id;
            christmas.StartDay = 1;
            christmas.StartMonth = Month.December;
            christmas.EndDay = 26;
            christmas.EndMonth = Month.December;
            _holidayRepository.AddHoliday(_dataSource, christmas);

            var johnLennon = Artist.Create("John Lennon");
            johnLennon.IsSolo = true;
            _artistRepository.AddArtist(_dataSource, johnLennon);

            var happyXmas = Song.Create("Happy Xmas (War is Over)", johnLennon);
            _happyXmasId = happyXmas.Id;
            happyXmas.Holiday = christmas;
            happyXmas.AboutTimeOfYear = TimeOfYear.Winter;
            _songRepository.AddSong(_dataSource, happyXmas);

            var cure = Artist.Create("The Cure");
            _artistRepository.AddArtist(_dataSource, cure);

            var fridayImInLove = Song.Create("Friday I'm in Love", cure);
            _fridayImInLoveId = fridayImInLove.Id;
            fridayImInLove.AboutDayOfWeek = DayOfWeek.Friday;
            _songRepository.AddSong(_dataSource, fridayImInLove);

            var fridayImInLoveRemix = Song.Create("Friday I'm in Love", cure);
            _fridayImInLoveRemixId = fridayImInLoveRemix.Id;
            fridayImInLoveRemix.IsRemix = true;
            fridayImInLoveRemix.Version = "Strangelove Remix";
            fridayImInLoveRemix.AboutDayOfWeek = DayOfWeek.Friday;
            fridayImInLoveRemix.OriginalSongId = _fridayImInLoveId;
            _songRepository.AddSong(_dataSource, fridayImInLoveRemix);

            var radiohead = Artist.Create("Radiohead");
            _artistRepository.AddArtist(_dataSource, radiohead);

            var creep = Song.Create("Creep", radiohead);
            _creepId = creep.Id;
            creep.HasExplicitContent = true;
            _songRepository.AddSong(_dataSource, creep);
        }

        /// <summary>
        /// Tests the get song method.
        /// </summary>
        [Test]
        public void GetSongTests()
        {
            var atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.AreEqual(_atomHeartMotherId, atomHeartMother.Id);
            Assert.AreEqual("Atom Heart Mother Suite", atomHeartMother.Name);
            Assert.AreEqual(_pinkFloydId, atomHeartMother.ArtistId);
            Assert.NotNull(atomHeartMother.Artist);
            Assert.AreEqual("Pink Floyd", atomHeartMother.Artist.Name);
            Assert.AreEqual("Atom Heart Mother Suite description", atomHeartMother.Description);
            Assert.AreEqual("Silence in the studio!", atomHeartMother.Lyrics);
            Assert.AreEqual(_classicRockId, atomHeartMother.GenreId);
            Assert.NotNull(atomHeartMother.Genre);
            Assert.AreEqual(_classicRockId, atomHeartMother.Genre.Id);
            Assert.AreEqual("Classic Rock", atomHeartMother.Genre.Name);
            Assert.NotNull(atomHeartMother.TimeSignature);
            Assert.AreEqual(MusicalKeys.C, atomHeartMother.Key);
            Assert.AreEqual("4/4", atomHeartMother.TimeSignature.ToString());
            Assert.AreEqual(69, atomHeartMother.Bpm);
            Assert.AreEqual(1424000, atomHeartMother.Length);
            Assert.IsNull(atomHeartMother.OriginalSongId);
            Assert.IsNull(atomHeartMother.AboutDayOfWeek);
            Assert.IsNull(atomHeartMother.AboutTimeOfYear);
            Assert.IsNull(atomHeartMother.BestPlayedAtStart);
            Assert.IsNull(atomHeartMother.BestPlayedAtStop);
            Assert.NotNull(atomHeartMother.Segments);
            Assert.AreEqual(2, atomHeartMother.Segments.Count);
            Assert.AreEqual("Father's Shout", atomHeartMother.Segments[0].Name);
            Assert.AreEqual(80, atomHeartMother.Segments[0].Bpm);
            Assert.AreEqual(MusicalKeys.G, atomHeartMother.Segments[1].Key);
            Assert.AreEqual("6/4", atomHeartMother.Segments[1].TimeSignature.ToString());
            Assert.NotNull(atomHeartMother.Credits);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);
            Assert.AreEqual("David Gilmour", atomHeartMother.Credits[0].Person.Name);
            Assert.AreEqual(2, atomHeartMother.Credits[0].Roles.Count);
            Assert.AreEqual("Lead Guitar", atomHeartMother.Credits[0].Roles[0].Name);
            Assert.AreEqual("Rhythm Guitar", atomHeartMother.Credits[0].Roles[1].Name);
            Assert.AreEqual("Nick Mason", atomHeartMother.Credits[1].Person.Name);
            Assert.AreEqual(1, atomHeartMother.Credits[1].Roles.Count);
            Assert.AreEqual("Drums", atomHeartMother.Credits[1].Roles[0].Name);
            Assert.AreEqual("Norman Smith", atomHeartMother.Credits[2].Person.Name);
            Assert.AreEqual(1, atomHeartMother.Credits[2].Roles.Count);
            Assert.AreEqual("Producer", atomHeartMother.Credits[2].Roles[0].Name);
            Assert.AreEqual("Richard Wright", atomHeartMother.Credits[3].Person.Name);
            Assert.AreEqual(1, atomHeartMother.Credits[3].Roles.Count);
            Assert.AreEqual("Keyboards", atomHeartMother.Credits[3].Roles[0].Name);
            Assert.AreEqual("Roger Waters", atomHeartMother.Credits[4].Person.Name);
            Assert.AreEqual(1, atomHeartMother.Credits[4].Roles.Count);
            Assert.AreEqual("Bass Guitar", atomHeartMother.Credits[4].Roles[0].Name);

            var fatOldSun = _songRepository.GetSong(_dataSource, _fatOldSunId);
            Assert.NotNull(fatOldSun);
            Assert.AreEqual(_fatOldSunId, fatOldSun.Id);
            Assert.AreEqual("Fat Old Sun", fatOldSun.Name);
            Assert.AreEqual(_pinkFloydId, atomHeartMother.ArtistId);
            Assert.NotNull(atomHeartMother.Artist);
            Assert.AreEqual("Pink Floyd", atomHeartMother.Artist.Name);
            Assert.IsNull(fatOldSun.Description);
            Assert.IsNull(fatOldSun.Lyrics);
            Assert.IsNull(fatOldSun.GenreId);
            Assert.IsNull(fatOldSun.Genre);
            Assert.IsNull(fatOldSun.TimeSignatureId);
            Assert.IsNull(fatOldSun.TimeSignature);
            Assert.IsNull(fatOldSun.Key);
            Assert.IsNull(fatOldSun.Bpm);
            Assert.AreEqual(0, fatOldSun.Length);
            Assert.IsNull(fatOldSun.OriginalSongId);
            Assert.IsNull(fatOldSun.AboutDayOfWeek);
            Assert.IsNull(fatOldSun.AboutTimeOfYear);
            Assert.IsNull(fatOldSun.BestPlayedAtStart);
            Assert.IsNull(fatOldSun.BestPlayedAtStop);
            Assert.IsNull(fatOldSun.Segments);
            Assert.IsNull(fatOldSun.Collaborators);
            Assert.IsNull(fatOldSun.Credits);

            var setTheControls = _songRepository.GetSong(_dataSource, _setTheControlsId);
            Assert.NotNull(setTheControls);
            Assert.AreEqual(_setTheControlsId, setTheControls.Id);
            Assert.AreEqual("Set the Controls for the Heart of the Sun", setTheControls.Name);
            Assert.AreEqual(_pinkFloydId, setTheControls.ArtistId);
            Assert.NotNull(setTheControls.Artist);
            Assert.AreEqual("Pink Floyd", setTheControls.Artist.Name);
            Assert.AreEqual(300000, setTheControls.BestPlayedAtStart);
            Assert.AreEqual(600000, setTheControls.BestPlayedAtStop);

            var setTheControlsLive = _songRepository.GetSong(_dataSource, _setTheControlsLiveId);
            Assert.NotNull(setTheControlsLive);
            Assert.AreEqual(_setTheControlsLiveId, setTheControlsLive.Id);
            Assert.AreEqual("Set the Controls for the Heart of the Sun", setTheControlsLive.Name);
            Assert.AreEqual(_pinkFloydId, setTheControlsLive.ArtistId);
            Assert.NotNull(setTheControlsLive.Artist);
            Assert.AreEqual("Pink Floyd", setTheControlsLive.Artist.Name);
            Assert.IsTrue(setTheControlsLive.IsLive);
            Assert.AreEqual(_setTheControlsId, setTheControlsLive.OriginalSongId);

            var lucyInTheSkyCover = _songRepository.GetSong(_dataSource, _lucyInTheSkyCoverId);
            Assert.NotNull(lucyInTheSkyCover);
            Assert.AreEqual(_lucyInTheSkyCoverId, lucyInTheSkyCover.Id);
            Assert.AreEqual("Lucy in the Sky with Diamonds", lucyInTheSkyCover.Name);
            Assert.NotNull(lucyInTheSkyCover.Artist);
            Assert.AreEqual("The Flaming Lips", lucyInTheSkyCover.Artist.Name);
            Assert.AreEqual(_lucyInTheSkyId, lucyInTheSkyCover.OriginalSongId);
            Assert.NotNull(lucyInTheSkyCover.Collaborators);
            Assert.AreEqual(2, lucyInTheSkyCover.Collaborators.Count);
            Assert.AreEqual("Miley Cyrus", lucyInTheSkyCover.Collaborators[0].Artist.Name);
            Assert.IsTrue(lucyInTheSkyCover.Collaborators[0].IsFeatured);
            Assert.AreEqual("Moby", lucyInTheSkyCover.Collaborators[1].Artist.Name);
            Assert.IsFalse(lucyInTheSkyCover.Collaborators[1].IsFeatured);

            var happyXmas = _songRepository.GetSong(_dataSource, _happyXmasId);
            Assert.NotNull(happyXmas);
            Assert.AreEqual(_happyXmasId, happyXmas.Id);
            Assert.AreEqual("Happy Xmas (War is Over)", happyXmas.Name);
            Assert.NotNull(happyXmas.Artist);
            Assert.AreEqual("John Lennon", happyXmas.Artist.Name);
            Assert.IsTrue(happyXmas.IsHoliday);
            Assert.AreEqual(_christmasId, happyXmas.HolidayId);
            Assert.NotNull(happyXmas.Holiday);
            Assert.AreEqual("Christmas", happyXmas.Holiday.Name);
            Assert.AreEqual(TimeOfYear.Winter, happyXmas.AboutTimeOfYear);

            var fridayImInLove = _songRepository.GetSong(_dataSource, _fridayImInLoveId);
            Assert.NotNull(fridayImInLove);
            Assert.AreEqual(_fridayImInLoveId, fridayImInLove.Id);
            Assert.AreEqual("Friday I'm in Love", fridayImInLove.Name);
            Assert.NotNull(fridayImInLove.Artist);
            Assert.AreEqual("The Cure", fridayImInLove.Artist.Name);
            Assert.AreEqual(DayOfWeek.Friday, fridayImInLove.AboutDayOfWeek);

            var fridayImInLoveRemix = _songRepository.GetSong(_dataSource, _fridayImInLoveRemixId);
            Assert.NotNull(fridayImInLoveRemix);
            Assert.AreEqual(_fridayImInLoveRemixId, fridayImInLoveRemix.Id);
            Assert.AreEqual("Friday I'm in Love", fridayImInLoveRemix.Name);
            Assert.NotNull(fridayImInLoveRemix.Artist);
            Assert.AreEqual("The Cure", fridayImInLoveRemix.Artist.Name);
            Assert.AreEqual(DayOfWeek.Friday, fridayImInLoveRemix.AboutDayOfWeek);
            Assert.IsTrue(fridayImInLoveRemix.IsRemix);
            Assert.AreEqual(_fridayImInLoveId, fridayImInLoveRemix.OriginalSongId);
            Assert.AreEqual("Strangelove Remix", fridayImInLoveRemix.Version);

            var creep = _songRepository.GetSong(_dataSource, _creepId);
            Assert.NotNull(creep);
            Assert.AreEqual(_creepId, creep.Id);
            Assert.AreEqual("Creep", creep.Name);
            Assert.NotNull(creep.Artist);
            Assert.AreEqual("Radiohead", creep.Artist.Name);
            Assert.IsTrue(creep.HasExplicitContent);
        }

        /// <summary>
        /// Tests the get recording session method.
        /// </summary>
        [Test]
        public void GetRecordingSessionTest()
        {
            var session = _songRepository.GetRecordingSession(_dataSource, _sessionId);
            Assert.NotNull(session);
            Assert.AreEqual(_atomHeartMotherId, session.SongId);
            Assert.AreEqual(DateTime.Parse("2/15/1970"), session.OccurredOn);
            Assert.AreEqual("Session One Notes", session.Notes);
        }

        /// <summary>
        /// Tests the list songs method.
        /// </summary>
        [Test]
        public void ListSongsTest()
        {
            var songs = _songRepository.ListSongs(_dataSource);
            Assert.NotNull(songs);
            Assert.AreEqual(10, songs.Count);

            Assert.AreEqual(_happyXmasId, songs[0].Id);
            Assert.AreEqual("Happy Xmas (War is Over)", songs[0].Name);
            Assert.AreEqual("John Lennon", songs[0].Artist.Name);
            Assert.IsNull(songs[0].Genre);
            Assert.AreEqual("Christmas", songs[0].Holiday.Name);
            Assert.IsNull(songs[0].TimeSignature);
            Assert.AreEqual(TimeOfYear.Winter, songs[0].AboutTimeOfYear);
            Assert.IsTrue(songs[0].IsHoliday);

            Assert.AreEqual(_atomHeartMotherId, songs[1].Id);
            Assert.AreEqual("Atom Heart Mother Suite", songs[1].Name);
            Assert.AreEqual("Pink Floyd", songs[1].Artist.Name);
            Assert.AreEqual("Classic Rock", songs[1].Genre.Name);
            Assert.IsNull(songs[1].Holiday);
            Assert.AreEqual("4/4", songs[1].TimeSignature.ToString());
            Assert.AreEqual(MusicalKeys.C, songs[1].Key);
            Assert.AreEqual(69, songs[1].Bpm);
            Assert.AreEqual(1424000, songs[1].Length);
            Assert.AreEqual("Atom Heart Mother Suite description", songs[1].Description);
            Assert.AreEqual("Silence in the studio!", songs[1].Lyrics);
            Assert.IsNull(songs[1].BestPlayedAtStart);
            Assert.IsNull(songs[1].BestPlayedAtStop);
            Assert.IsFalse(songs[1].IsHoliday);
            Assert.IsFalse(songs[1].IsLive);
            Assert.IsFalse(songs[1].HasExplicitContent);
            Assert.IsNull(songs[1].AboutTimeOfYear);
            Assert.IsNull(songs[1].AboutDayOfWeek);
            Assert.IsFalse(songs[1].IsCover);

            Assert.AreEqual(_fatOldSunId, songs[2].Id);
            Assert.AreEqual("Fat Old Sun", songs[2].Name);
            Assert.AreEqual("Pink Floyd", songs[2].Artist.Name);
            Assert.IsNull(songs[2].Genre);
            Assert.IsNull(songs[2].Holiday);
            Assert.IsNull(songs[2].TimeSignature);

            Assert.AreEqual(_setTheControlsId, songs[3].Id);
            Assert.AreEqual("Set the Controls for the Heart of the Sun", songs[3].Name);
            Assert.AreEqual("Pink Floyd", songs[3].Artist.Name);
            Assert.AreEqual("Classic Rock", songs[3].Genre.Name);
            Assert.IsNull(songs[3].Holiday);
            Assert.IsNull(songs[3].TimeSignature);
            Assert.AreEqual(300000, songs[3].BestPlayedAtStart);
            Assert.AreEqual(600000, songs[3].BestPlayedAtStop);

            Assert.AreEqual(_setTheControlsLiveId, songs[4].Id);
            Assert.AreEqual("Set the Controls for the Heart of the Sun", songs[4].Name);
            Assert.AreEqual("Pink Floyd", songs[4].Artist.Name);
            Assert.AreEqual("Classic Rock", songs[4].Genre.Name);
            Assert.IsNull(songs[4].Holiday);
            Assert.IsNull(songs[4].TimeSignature);
            Assert.IsTrue(songs[4].IsLive);
            Assert.AreEqual(_setTheControlsId, songs[4].OriginalSongId);

            Assert.AreEqual(_creepId, songs[5].Id);
            Assert.AreEqual("Creep", songs[5].Name);
            Assert.AreEqual("Radiohead", songs[5].Artist.Name);
            Assert.IsNull(songs[5].Genre);
            Assert.IsNull(songs[5].Holiday);
            Assert.IsNull(songs[5].TimeSignature);
            Assert.IsTrue(songs[5].HasExplicitContent);

            Assert.AreEqual(_lucyInTheSkyId, songs[6].Id);
            Assert.AreEqual("Lucy in the Sky with Diamonds", songs[6].Name);
            Assert.AreEqual("The Beatles", songs[6].Artist.Name);
            Assert.IsNull(songs[6].Genre);
            Assert.IsNull(songs[6].Holiday);
            Assert.IsNull(songs[6].TimeSignature);

            Assert.AreEqual(_fridayImInLoveId, songs[7].Id);
            Assert.AreEqual("Friday I'm in Love", songs[7].Name);
            Assert.AreEqual("The Cure", songs[7].Artist.Name);
            Assert.IsNull(songs[7].Genre);
            Assert.IsNull(songs[7].Holiday);
            Assert.IsNull(songs[7].TimeSignature);
            Assert.AreEqual(DayOfWeek.Friday, songs[7].AboutDayOfWeek);
            Assert.IsFalse(songs[7].IsRemix);

            Assert.AreEqual(_fridayImInLoveRemixId, songs[8].Id);
            Assert.AreEqual("Friday I'm in Love", songs[8].Name);
            Assert.AreEqual("Strangelove Remix", songs[8].Version);
            Assert.AreEqual("The Cure", songs[8].Artist.Name);
            Assert.IsNull(songs[8].Genre);
            Assert.IsNull(songs[8].Holiday);
            Assert.IsNull(songs[8].TimeSignature);
            Assert.AreEqual(DayOfWeek.Friday, songs[8].AboutDayOfWeek);
            Assert.IsTrue(songs[8].IsRemix);
            Assert.AreEqual(_fridayImInLoveId, songs[8].OriginalSongId);

            Assert.AreEqual(_lucyInTheSkyCoverId, songs[9].Id);
            Assert.AreEqual("Lucy in the Sky with Diamonds", songs[9].Name);
            Assert.AreEqual("The Flaming Lips", songs[9].Artist.Name);
            Assert.IsNull(songs[9].Genre);
            Assert.IsNull(songs[9].Holiday);
            Assert.IsNull(songs[9].TimeSignature);
            Assert.IsTrue(songs[9].IsCover);
            Assert.AreEqual(_lucyInTheSkyId, songs[9].OriginalSongId);
        }

        /// <summary>
        /// Test the list recording sessions method.
        /// </summary>
        [Test]
        public void ListRecordingSessionsTest()
        {
            var sessions = _songRepository.ListRecordingSessions(_dataSource, _atomHeartMotherId);
            Assert.NotNull(sessions);
            Assert.AreEqual(3, sessions.Count);

            Assert.AreEqual(_atomHeartMotherId, sessions[0].SongId);
            Assert.AreEqual(DateTime.Parse("2/15/1970"), sessions[0].OccurredOn);
            Assert.AreEqual("Session One Notes", sessions[0].Notes);

            Assert.AreEqual(_atomHeartMotherId, sessions[1].SongId);
            Assert.AreEqual(DateTime.Parse("2/20/1970"), sessions[1].OccurredOn);
            Assert.AreEqual("Session Two Notes", sessions[1].Notes);

            Assert.AreEqual(_atomHeartMotherId, sessions[2].SongId);
            Assert.AreEqual(DateTime.Parse("3/1/1970"), sessions[2].OccurredOn);
            Assert.AreEqual("Session Three Notes", sessions[2].Notes);
        }

        /// <summary>
        /// Tests the delete song method.
        /// </summary>
        [Test]
        public void DeleteSongTest()
        {
            var pinkFloyd = _artistRepository.GetArtist(_dataSource, _pinkFloydId);
            var saucerfulOfSecrets = Song.Create("A Saucerful of Secrets", pinkFloyd);
            var saucerfulOfSecretsId = saucerfulOfSecrets.Id;
            _songRepository.AddSong(_dataSource, saucerfulOfSecrets);

            var segment1 = SongSegment.Create(saucerfulOfSecrets, 0, 12034);
            segment1.Bpm = 80;
            _songRepository.AddSongSegment(_dataSource, segment1);
            var segment2 = SongSegment.Create(saucerfulOfSecrets, 12034, 18345);
            segment2.Key = MusicalKeys.G;
            _songRepository.AddSongSegment(_dataSource, segment2);

            var rogerCredit = SongCredit.Create(saucerfulOfSecrets, pinkFloyd.Members[0].Person);
            _songRepository.AddSongCredit(_dataSource, rogerCredit);
            _songRepository.AddSongCreditRole(_dataSource, rogerCredit, pinkFloyd.Members[0].Roles[0]);
            var davidCredit = SongCredit.Create(saucerfulOfSecrets, pinkFloyd.Members[1].Person);
            _songRepository.AddSongCredit(_dataSource, davidCredit);
            _songRepository.AddSongCreditRole(_dataSource, davidCredit, pinkFloyd.Members[1].Roles[0]);
            _songRepository.AddSongCreditRole(_dataSource, davidCredit, pinkFloyd.Members[1].Roles[1]);
            var nickCredit = SongCredit.Create(saucerfulOfSecrets, pinkFloyd.Members[2].Person);
            _songRepository.AddSongCredit(_dataSource, nickCredit);
            _songRepository.AddSongCreditRole(_dataSource, nickCredit, pinkFloyd.Members[2].Roles[0]);
            var richardCredit = SongCredit.Create(saucerfulOfSecrets, pinkFloyd.Members[3].Person);
            _songRepository.AddSongCredit(_dataSource, richardCredit);
            _songRepository.AddSongCreditRole(_dataSource, richardCredit, pinkFloyd.Members[3].Roles[0]);

            var collaborator = Artist.Create("The International Staff Band");
            _artistRepository.AddArtist(_dataSource, collaborator);
            _songRepository.AddSongCollaborator(_dataSource, SongCollaborator.Create(saucerfulOfSecrets, collaborator));

            var song = _songRepository.GetSong(_dataSource, saucerfulOfSecretsId);
            Assert.NotNull(song);
            Assert.NotNull(song.Collaborators);
            Assert.AreEqual(1, song.Collaborators.Count);
            Assert.NotNull(song.Segments);
            Assert.AreEqual(2, song.Segments.Count);
            Assert.NotNull(song.Credits);
            Assert.AreEqual(4, song.Credits.Count);
            Assert.NotNull(song.Credits[0].Roles);
            Assert.AreEqual(2, song.Credits[0].Roles.Count);
            Assert.NotNull(song.Credits[1].Roles);
            Assert.AreEqual(1, song.Credits[1].Roles.Count);
            Assert.NotNull(song.Credits[2].Roles);
            Assert.AreEqual(1, song.Credits[2].Roles.Count);
            Assert.NotNull(song.Credits[3].Roles);
            Assert.AreEqual(1, song.Credits[3].Roles.Count);

            _songRepository.DeleteSong(_dataSource, song);

            var deletedSong = _songRepository.GetSong(_dataSource, saucerfulOfSecretsId);
            Assert.IsNull(deletedSong);
        }

        /// <summary>
        /// Tests the delete song segment method.
        /// </summary>
        [Test]
        public void DeleteSongSegmentTest()
        {
            var lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.IsNull(lucyInTheSky.Segments);

            var twoFour = TimeSignature.Create(2, BeatLength.Quarter);
            _timeSignatureRepository.AddTimeSignature(_dataSource, twoFour);
            var segment1 = SongSegment.Create(lucyInTheSky, 0, 56789);
            segment1.TimeSignature = twoFour;
            _songRepository.AddSongSegment(_dataSource, segment1);

            var fourFour = _timeSignatureRepository.GetTimeSignature(_dataSource, 4, BeatLength.Quarter);
            var segment2 = SongSegment.Create(lucyInTheSky, 56789, 98789);
            segment2.TimeSignature = fourFour;
            _songRepository.AddSongSegment(_dataSource, segment2);

            lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.NotNull(lucyInTheSky.Segments);
            Assert.AreEqual(2, lucyInTheSky.Segments.Count);

            _songRepository.DeleteSongSegment(_dataSource, lucyInTheSky.Segments[0]);

            lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.NotNull(lucyInTheSky.Segments);
            Assert.AreEqual(1, lucyInTheSky.Segments.Count);

            _songRepository.DeleteSongSegment(_dataSource, lucyInTheSky.Segments[0]);

            lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.IsNull(lucyInTheSky.Segments);
        }

        /// <summary>
        /// Tests the delete collaborator method.
        /// </summary>
        [Test]
        public void DeleteSongCollaboratorTest()
        {
            var lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.IsNull(lucyInTheSky.Collaborators);

            var pinkFloyd = _artistRepository.GetArtist(_dataSource, _pinkFloydId);
            var collaborator = SongCollaborator.Create(lucyInTheSky, pinkFloyd);
            _songRepository.AddSongCollaborator(_dataSource, collaborator);

            lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.NotNull(lucyInTheSky.Collaborators);
            Assert.AreEqual(1, lucyInTheSky.Collaborators.Count);

            _songRepository.DeleteSongCollaborator(_dataSource, lucyInTheSky.Collaborators[0]);

            lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            Assert.IsNull(lucyInTheSky.Collaborators);
        }

        /// <summary>
        /// Tests the delete song credit role method.
        /// </summary>
        [Test]
        public void DeleteSongCreditRoleTest()
        {
            var atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);

            var normanCredit = atomHeartMother.Credits.SingleOrDefault(c => c.Person.Name == "Norman Smith");
            Assert.NotNull(normanCredit);
            Assert.AreEqual(1, normanCredit.Roles.Count);

            var keyboards = _roleRepository.GetRole(_dataSource, "Keyboards");
            _songRepository.AddSongCreditRole(_dataSource, normanCredit, keyboards);

            atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            normanCredit = atomHeartMother.Credits.SingleOrDefault(c => c.Person.Name == "Norman Smith");
            Assert.NotNull(normanCredit);
            Assert.AreEqual(2, normanCredit.Roles.Count);

            _songRepository.DeleteSongCreditRole(_dataSource, normanCredit, keyboards);

            _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            normanCredit = atomHeartMother.Credits.SingleOrDefault(c => c.Person.Name == "Norman Smith");
            Assert.NotNull(normanCredit);
            Assert.AreEqual(1, normanCredit.Roles.Count);
            Assert.AreNotEqual(keyboards.Id, normanCredit.Roles[0].Id);
        }

        /// <summary>
        /// Tests the delete song credit method.
        /// </summary>
        [Test]
        public void DeleteSongCreditTest()
        {
            var atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);

            var johnLennon = Person.Create("John Lennon");
            _personRepository.AddPerson(_dataSource, johnLennon);

            var keyboards = _roleRepository.GetRole(_dataSource, "Keyboards");
            var johnCredit = SongCredit.Create(atomHeartMother, johnLennon);
            johnCredit.Roles.Add(keyboards);
            _songRepository.AddSongCredit(_dataSource, johnCredit);

            atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            Assert.AreEqual(6, atomHeartMother.Credits.Count);
            johnCredit = atomHeartMother.Credits.SingleOrDefault(c => c.PersonId == johnLennon.Id);
            Assert.NotNull(johnCredit);

            _songRepository.DeleteSongCredit(_dataSource, johnCredit);

            atomHeartMother = _songRepository.GetSong(_dataSource, _atomHeartMotherId);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);
        }

        /// <summary>
        /// Tests the delete recording session method.
        /// </summary>
        [Test]
        public void DeleteRecordingSessionTest()
        {
            var lucyInTheSky = _songRepository.GetSong(_dataSource, _lucyInTheSkyId);
            var session = RecordingSession.Create(lucyInTheSky, DateTime.Parse("2/1/1967"));
            session.Notes = "Lucy session note.";
            _songRepository.AddRecordingSession(_dataSource, session);

            var sessions = _songRepository.ListRecordingSessions(_dataSource, _lucyInTheSkyId);
            Assert.NotNull(sessions);
            Assert.AreEqual(1, sessions.Count);
            Assert.AreEqual(session.Id, sessions[0].Id);

            _songRepository.DeleteRecordingSession(_dataSource, sessions[0]);

            sessions = _songRepository.ListRecordingSessions(_dataSource, _lucyInTheSkyId);
            Assert.IsFalse(sessions.Any());
        }
    }
}
