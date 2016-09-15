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
    /// Tests the album repository class.
    /// </summary>
    [TestFixture]
    public class AlbumRepositoryTests
    {
        private const string TestFileName = "AlbumRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private ILabelRepository _labelRepository;
        private IHolidayRepository _holidayRepository;
        private Guid _capitolId;
        private Guid _warnerBrothersId;
        private Guid _christmasId;
        private Guid _pinkFloydId;
        private Guid _beatlesId;
        private Guid _flamingLipsId;
        private Guid _meddleId;
        private Guid _atomHeartMotherId;
        private Guid _sgtPepperId;
        private Guid _magicalMysteryTourId;
        private Guid _whiteAlbumId;
        private Guid _helpFromMyFwendsId;
        private Guid _helpFromMyFwendsReleaseId;
        private Guid _pulpFictionSoundtrackId;
        private Guid _girlYoullBeAWomanSoonId;
        private Guid _beachBoysChristmasId;
        private Guid _yearZeroId;
        private Guid _yearZeroRemixedId;
        private Guid _bandOfGypsysId;
        private Guid _letDownId;
        private Guid _globalUnderground7Id;
        private Guid _friendId;

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
            _holidayRepository = new HolidayRepository();
            _songRepository = new SongRepository(_roleRepository, _personRepository);
            _labelRepository = new LabelRepository(_roleRepository, _personRepository);
            _albumRepository = new AlbumRepository(_roleRepository, _personRepository);

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
            var christmas = Holiday.Create("Christmas");
            _christmasId = christmas.Id;
            christmas.StartDay = 1;
            christmas.StartMonth = Month.December;
            christmas.EndDay = 26;
            christmas.EndMonth = Month.December;
            _holidayRepository.AddHoliday(_dataSource, christmas);

            var friend = Person.Create("Friend");
            _friendId = friend.Id;
            _personRepository.AddPerson(_dataSource, friend);

            var leadVocals = Role.Create("Lead Vocals");
            _roleRepository.AddRole(_dataSource, leadVocals);
            var backingVocals = Role.Create("Backing Vocals");
            _roleRepository.AddRole(_dataSource, backingVocals);
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
            var founder = Role.Create("Founder");
            _roleRepository.AddRole(_dataSource, founder);
            var director = Role.Create("Director");
            _roleRepository.AddRole(_dataSource, director);
            var president = Role.Create("President");
            _roleRepository.AddRole(_dataSource, president);

            var capitol = Label.Create("Capitol");
            _capitolId = capitol.Id;
            capitol.Bio = "Capitol Bio";
            capitol.FoundedOn = DateTime.Parse("2/2/1942");
            capitol.LogoFilePath = "c:\\images\\capitol.jpg";
            _labelRepository.AddLabel(_dataSource, capitol);
            var johnnyMercer = Person.Create("Johnny Mercer");
            _personRepository.AddPerson(_dataSource, johnnyMercer);
            var johnnyPersonnel = LabelPersonnel.Create(capitol, johnnyMercer, 1);
            _labelRepository.AddLabelPersonnel(_dataSource, johnnyPersonnel);
            _labelRepository.AddLabelPersonnelRole(_dataSource, johnnyPersonnel, founder);
            _labelRepository.AddLabelPersonnelRole(_dataSource, johnnyPersonnel, president);
            var buddyDeSylva = Person.Create("Buddy DeSylva");
            _personRepository.AddPerson(_dataSource, buddyDeSylva);
            var buddyPersonnel = LabelPersonnel.Create(capitol, buddyDeSylva, 2);
            _labelRepository.AddLabelPersonnel(_dataSource, buddyPersonnel);
            _labelRepository.AddLabelPersonnelRole(_dataSource, buddyPersonnel, director);

            var pinkFloyd = Artist.Create("Pink Floyd");
            _pinkFloydId = pinkFloyd.Id;
            _artistRepository.AddArtist(_dataSource, pinkFloyd);

            var rogerWaters = Person.Create("Roger Waters");
            _personRepository.AddPerson(_dataSource, rogerWaters);
            var rogerMember = ArtistMember.Create(pinkFloyd, rogerWaters, 1);
            _artistRepository.AddArtistMember(_dataSource, rogerMember);
            _artistRepository.AddArtistMemberRole(_dataSource, rogerMember, bassGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, rogerMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, rogerMember, backingVocals);
            var davidGilmour = Person.Create("David Gilmour");
            _personRepository.AddPerson(_dataSource, davidGilmour);
            var davidMember = ArtistMember.Create(pinkFloyd, davidGilmour, 2);
            _artistRepository.AddArtistMember(_dataSource, davidMember);
            _artistRepository.AddArtistMemberRole(_dataSource, davidMember, leadGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, davidMember, rhythmGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, davidMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, davidMember, backingVocals);
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
            _artistRepository.AddArtistMemberRole(_dataSource, richardMember, backingVocals);           

            var atomHeartMother = Album.Create("Atom Heart Mother", pinkFloyd);
            _atomHeartMotherId = atomHeartMother.Id;
            atomHeartMother.FirstReleasedOn = DateTime.Parse("2/10/1970");
            atomHeartMother.Description = "Atom Heart Mother Description";
            atomHeartMother.LinerNotes = "Atom Heart Mother Liner Notes";
            atomHeartMother.Length = 123456;
            atomHeartMother.LengthType = LengthType.LP;
            atomHeartMother.DiscCount = 1;
            atomHeartMother.TrackCounts = "5";
            atomHeartMother.Rating = 5;
            _albumRepository.AddAlbum(_dataSource, atomHeartMother);
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(atomHeartMother, "c:\\images\\atom-heart-mother-front.jpg", 1));
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(atomHeartMother, "c:\\images\\atom-heart-mother-back.jpg", 2));
            var atomHeartMotherSuite = Song.Create("Atom Heart Mother Suite", pinkFloyd);
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
            _songRepository.AddSong(_dataSource, alansPsychedelicBreakfast);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(atomHeartMother, alansPsychedelicBreakfast, 5, 'B'));
            var rogerAlbumCredit = AlbumCredit.Create(atomHeartMother, rogerWaters);
            _albumRepository.AddAlbumCredit(_dataSource, rogerAlbumCredit);
            _albumRepository.AddAlbumCreditRole(_dataSource, rogerAlbumCredit, bassGuitar);
            _albumRepository.AddAlbumCreditRole(_dataSource, rogerAlbumCredit, leadVocals);
            _albumRepository.AddAlbumCreditRole(_dataSource, rogerAlbumCredit, backingVocals);
            var davidAlbumCredit = AlbumCredit.Create(atomHeartMother, davidGilmour);
            _albumRepository.AddAlbumCredit(_dataSource, davidAlbumCredit);
            _albumRepository.AddAlbumCreditRole(_dataSource, davidAlbumCredit, leadGuitar);
            _albumRepository.AddAlbumCreditRole(_dataSource, davidAlbumCredit, rhythmGuitar);
            _albumRepository.AddAlbumCreditRole(_dataSource, davidAlbumCredit, leadVocals);
            _albumRepository.AddAlbumCreditRole(_dataSource, davidAlbumCredit, backingVocals);
            var nickAlbumCredit = AlbumCredit.Create(atomHeartMother, nickMason);
            _albumRepository.AddAlbumCredit(_dataSource, nickAlbumCredit);
            _albumRepository.AddAlbumCreditRole(_dataSource, nickAlbumCredit, drums);
            var richardAlbumCredit = AlbumCredit.Create(atomHeartMother, richardWright);
            _albumRepository.AddAlbumCredit(_dataSource, richardAlbumCredit);
            _albumRepository.AddAlbumCreditRole(_dataSource, richardAlbumCredit, keyboards);
            var normanSmith = Person.Create("Norman Smith");
            _personRepository.AddPerson(_dataSource, normanSmith);
            var normanCredit = AlbumCredit.Create(atomHeartMother, normanSmith);
            _albumRepository.AddAlbumCredit(_dataSource, normanCredit);
            _albumRepository.AddAlbumCreditRole(_dataSource, normanCredit, producer);

            var meddle = Album.Create("Meddle", pinkFloyd);
            _meddleId = meddle.Id;
            _albumRepository.AddAlbum(_dataSource, meddle);

            var beatles = Artist.Create("The Beatles");
            _beatlesId = beatles.Id;
            _artistRepository.AddArtist(_dataSource, beatles);

            var johnLennon = Person.Create("John Lennon");
            _personRepository.AddPerson(_dataSource, johnLennon);
            var johnMember = ArtistMember.Create(beatles, johnLennon, 1);
            _artistRepository.AddArtistMember(_dataSource, johnMember);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, johnMember, rhythmGuitar);
            var paulMccartney = Person.Create("Paul McCartney");
            _personRepository.AddPerson(_dataSource, paulMccartney);
            var paulMember = ArtistMember.Create(beatles, paulMccartney, 2);
            _artistRepository.AddArtistMember(_dataSource, paulMember);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, rhythmGuitar);
            _artistRepository.AddArtistMemberRole(_dataSource, paulMember, bassGuitar);
            var georgeHarrison = Person.Create("George Harrison");
            _personRepository.AddPerson(_dataSource, georgeHarrison);
            var georgeMember = ArtistMember.Create(beatles, georgeHarrison, 3);
            _artistRepository.AddArtistMember(_dataSource, georgeMember);
            _artistRepository.AddArtistMemberRole(_dataSource, georgeMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, georgeMember, backingVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, georgeMember, leadGuitar);
            var ringoStarr = Person.Create("Ringo Starr");
            _personRepository.AddPerson(_dataSource, ringoStarr);
            var ringoMember = ArtistMember.Create(beatles, ringoStarr, 4);
            _artistRepository.AddArtistMember(_dataSource, ringoMember);
            _artistRepository.AddArtistMemberRole(_dataSource, ringoMember, leadVocals);
            _artistRepository.AddArtistMemberRole(_dataSource, ringoMember, drums);

            var sgtPepper = Album.Create("Sgt. Pepper's Lonely Hearts Club Band", beatles);
            _sgtPepperId = sgtPepper.Id;
            sgtPepper.DiscCount = 1;
            sgtPepper.TrackCounts = "13";
            sgtPepper.FirstReleasedOn = DateTime.Parse("6/1/1967");
            _albumRepository.AddAlbum(_dataSource, sgtPepper);
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(sgtPepper, "c:\\images\\sgt-pepper-front.jpg", 1));
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(sgtPepper, "c:\\images\\sgt-pepper-inner.jpg", 2));
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(sgtPepper, "c:\\images\\sgt-pepper-back.jpg", 3));
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
            _songRepository.AddSong(_dataSource, aDayInTheLife);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(sgtPepper, aDayInTheLife, 13, 'B'));
            var sgtPepperRelease1 = AlbumRelease.Create(sgtPepper, DateTime.Parse("6/1/1967"), MediaTypes.Vinyl, capitol, "SMAS 2653");
            _albumRepository.AddAlbumRelease(_dataSource, sgtPepperRelease1);
            var sgtPepperMedia1 = AlbumMedia.Create(sgtPepper, MediaTypes.Vinyl);
            sgtPepperMedia1.MediaCondition = Condition.GoodPlus;
            sgtPepperMedia1.PackagingCondition = Condition.Good;
            sgtPepperMedia1.PurchasedOn = DateTime.Parse("5/5/1985");
            sgtPepperMedia1.PurchasedPrice = 10.95m;
            sgtPepperMedia1.ApproximateValue = 15.99m;
            sgtPepperMedia1.Release = sgtPepperRelease1;
            sgtPepperMedia1.OnLoanToPersonId = friend.Id;
            _albumRepository.AddAlbumMedia(_dataSource, sgtPepperMedia1);
            var sgtPepperRelease2 = AlbumRelease.Create(sgtPepper, DateTime.Parse("11/16/2010"), DigitalFormats.M4A);
            _albumRepository.AddAlbumRelease(_dataSource, sgtPepperRelease2);
            var sgtPepperMedia2 = AlbumMedia.Create(sgtPepper, DigitalFormats.M4A);
            sgtPepperMedia2.Release = sgtPepperRelease2;
            _albumRepository.AddAlbumMedia(_dataSource, sgtPepperMedia2);
            var sgtPepperMedia3 = AlbumMedia.Create(sgtPepper, MediaTypes.CD | MediaTypes.DVD);
            _albumRepository.AddAlbumMedia(_dataSource, sgtPepperMedia3);

            var magicalMysteryTour = Album.Create("Magical Mystery Tour", beatles);
            _magicalMysteryTourId = magicalMysteryTour.Id;
            magicalMysteryTour.FirstReleasedOn = DateTime.Parse("11/19/1967");
            _albumRepository.AddAlbum(_dataSource, magicalMysteryTour);
            var magicalMysteryTourRelease1 = AlbumRelease.Create(magicalMysteryTour, DateTime.Parse("11/19/1967"), MediaTypes.Vinyl);
            magicalMysteryTourRelease1.LabelId = _capitolId;
            magicalMysteryTourRelease1.CatalogNumber = "MAL 2835";
            _albumRepository.AddAlbumRelease(_dataSource, magicalMysteryTourRelease1);
            var magicalMysteryTourRelease2 = AlbumRelease.Create(magicalMysteryTour, DateTime.Parse("11/27/1967"), MediaTypes.Vinyl);
            magicalMysteryTourRelease2.LabelId = _capitolId;
            _albumRepository.AddAlbumRelease(_dataSource, magicalMysteryTourRelease2);
            magicalMysteryTourRelease2.CatalogNumber = "SMAL 2835";
            var magicalMysteryTourRelease3 = AlbumRelease.Create(magicalMysteryTour, DateTime.Parse("12/8/1967"), MediaTypes.Vinyl);
            magicalMysteryTourRelease3.LabelId = _capitolId;
            magicalMysteryTourRelease3.CatalogNumber = "SMAL-2835";
            _albumRepository.AddAlbumRelease(_dataSource, magicalMysteryTourRelease3);

            var whiteAlbum = Album.Create("The Beatles", beatles);
            whiteAlbum.FirstReleasedOn = DateTime.Parse("11/27/1967");
            _whiteAlbumId = whiteAlbum.Id;
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

            var warnerBrothers = Label.Create("Warner Bros. Records");
            _warnerBrothersId = warnerBrothers.Id;
            _labelRepository.AddLabel(_dataSource, warnerBrothers);
            var flamingLips = Artist.Create("The Flaming Lips");
            _flamingLipsId = flamingLips.Id;
            _artistRepository.AddArtist(_dataSource, flamingLips);
            var helpFromMyFwends = Album.Create("With a Little Help from My Fwends", flamingLips);
            _helpFromMyFwendsId = helpFromMyFwends.Id;
            helpFromMyFwends.TributeArtistId = beatles.Id;
            helpFromMyFwends.OriginalAlbumId = sgtPepper.Id;
            helpFromMyFwends.FirstReleasedOn = DateTime.Parse("10/28/14");
            _albumRepository.AddAlbum(_dataSource, helpFromMyFwends);
            var helpFromMyFwendsRelease = AlbumRelease.Create(helpFromMyFwends, DateTime.Parse("10/28/14"),
                MediaTypes.CD | MediaTypes.Vinyl | MediaTypes.Digital, DigitalFormats.MP3 | DigitalFormats.WAV);
            _helpFromMyFwendsReleaseId = helpFromMyFwendsRelease.Id;
            helpFromMyFwendsRelease.CatalogNumber = "544000-1";
            helpFromMyFwendsRelease.LabelId = _warnerBrothersId;
            _albumRepository.AddAlbumRelease(_dataSource, helpFromMyFwendsRelease);
            var myMorningJacket = Artist.Create("My Morning Jacket");
            _artistRepository.AddArtist(_dataSource, myMorningJacket);
            _albumRepository.AddAlbumCollaborator(_dataSource, AlbumCollaborator.Create(helpFromMyFwends, myMorningJacket));
            var moby = Artist.Create("Moby");
            _artistRepository.AddArtist(_dataSource, moby);
            _albumRepository.AddAlbumCollaborator(_dataSource, AlbumCollaborator.Create(helpFromMyFwends, moby));
            var phantogram = Artist.Create("Phantogram");
            _artistRepository.AddArtist(_dataSource, phantogram);
            var phantogramCollaborator = AlbumCollaborator.Create(helpFromMyFwends, phantogram);
            phantogramCollaborator.IsFeatured = true;
            _albumRepository.AddAlbumCollaborator(_dataSource, phantogramCollaborator);

            var pulpFictionSoundtrack = Album.Create("Pulp Fiction Original Soundtrack");
            _pulpFictionSoundtrackId = pulpFictionSoundtrack.Id;
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
            _girlYoullBeAWomanSoonId = girlYoullBeAWomanSoon.Id;
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
            var flowersOnTheWall = Song.Create("Flowers on the Wall", statlerBrothers);
            _songRepository.AddSong(_dataSource, flowersOnTheWall);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(pulpFictionSoundtrack, flowersOnTheWall, 13));
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

            var beachBoys = Artist.Create("The Beach Boys");
            _artistRepository.AddArtist(_dataSource, beachBoys);
            var beachBoysChristmas = Album.Create("The Beach Boys' Christmas Album", beachBoys);
            _beachBoysChristmasId = beachBoysChristmas.Id;
            beachBoysChristmas.Holiday = christmas;
            _albumRepository.AddAlbum(_dataSource, beachBoysChristmas);

            var nineInchNails = Artist.Create("Nine Inch Nails");
            _artistRepository.AddArtist(_dataSource, nineInchNails);
            var yearZero = Album.Create("Year Zero", nineInchNails);
            _yearZeroId = yearZero.Id;
            _albumRepository.AddAlbum(_dataSource, yearZero);
            var yearZeroRemixed = Album.Create("Y34RZ3R0R3M1X3D", nineInchNails);
            _yearZeroRemixedId = yearZeroRemixed.Id;
            yearZeroRemixed.IsRemix = true;
            yearZeroRemixed.IsMixedArtist = true;
            yearZeroRemixed.OriginalAlbumId = yearZero.Id;
            _albumRepository.AddAlbum(_dataSource, yearZeroRemixed);

            var jimiHendrix = Artist.Create("Jimi Hendrix");
            _artistRepository.AddArtist(_dataSource, jimiHendrix);
            var bandOfGypsys = Album.Create("Band of Gypsys", jimiHendrix);
            _bandOfGypsysId = bandOfGypsys.Id;
            bandOfGypsys.IsLive = true;
            _albumRepository.AddAlbum(_dataSource, bandOfGypsys);

            var radiohead = Artist.Create("Radiohead");
            _artistRepository.AddArtist(_dataSource, radiohead);
            var letDown = Album.Create("Let Down", radiohead);
            _letDownId = letDown.Id;
            letDown.Edition = "DJ Promo";
            letDown.LengthType = LengthType.Single;
            letDown.IsPromotional = true;
            letDown.IsBootleg = true;
            _albumRepository.AddAlbum(_dataSource, letDown);

            var paulOakenfold = Artist.Create("Paul Oakenfold");
            _artistRepository.AddArtist(_dataSource, paulOakenfold);
            var globalUnderground7 = Album.Create("Global Underground 007: New York", paulOakenfold);
            _globalUnderground7Id = globalUnderground7.Id;
            globalUnderground7.IsCompilation = true;
            globalUnderground7.IsMixedArtist = true;
            globalUnderground7.IsSingleTrack = true;
            _albumRepository.AddAlbum(_dataSource, globalUnderground7);
            var mystica = Artist.Create("Mystica");
            _artistRepository.AddArtist(_dataSource, mystica);
            var bliss = Song.Create("Bliss", mystica);
            bliss.Version = "Mystica Mix";
            _songRepository.AddSong(_dataSource, bliss);
            var blissTrack = AlbumTrack.Create(globalUnderground7, bliss, 1);
            blissTrack.StartTime = 0;
            blissTrack.StopTime = 355000;
            _albumRepository.AddAlbumTrack(_dataSource, blissTrack);
            var jamieMyerson = Artist.Create("Jamie Myerson");
            _artistRepository.AddArtist(_dataSource, jamieMyerson);
            var rescueMe = Song.Create("Rescue Me", jamieMyerson);
            _songRepository.AddSong(_dataSource, rescueMe);
            var rescueMeTrack = AlbumTrack.Create(globalUnderground7, rescueMe, 2);
            rescueMeTrack.StartTime = 355000;
            rescueMeTrack.StopTime = 583000;
            _albumRepository.AddAlbumTrack(_dataSource, rescueMeTrack);
            var tasteExperience = Artist.Create("Taste Experience");
            _artistRepository.AddArtist(_dataSource, tasteExperience);
            var summersault = Song.Create("Summersault", tasteExperience);
            _songRepository.AddSong(_dataSource, summersault);
            var summersaultTrack = AlbumTrack.Create(globalUnderground7, summersault, 3);
            summersaultTrack.StartTime = 583000;
            summersaultTrack.StopTime = 1025000;
            _albumRepository.AddAlbumTrack(_dataSource, summersaultTrack);
        }

        /// <summary>
        /// Tests the get album method.
        /// </summary>
        [Test]
        public void GetAlbumTests()
        {
            var atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.AreEqual(_atomHeartMotherId, atomHeartMother.Id);
            Assert.AreEqual("Atom Heart Mother", atomHeartMother.Name);
            Assert.AreEqual(_pinkFloydId, atomHeartMother.ArtistId);
            Assert.NotNull(atomHeartMother.Artist);
            Assert.AreEqual("Pink Floyd", atomHeartMother.Artist.Name);
            Assert.AreEqual(1, atomHeartMother.DiscCount);
            Assert.AreEqual("5", atomHeartMother.TrackCounts);
            Assert.AreEqual(DateTime.Parse("2/10/1970"), atomHeartMother.FirstReleasedOn);
            Assert.AreEqual("Atom Heart Mother Description", atomHeartMother.Description);
            Assert.AreEqual("Atom Heart Mother Liner Notes", atomHeartMother.LinerNotes);
            Assert.AreEqual(5, atomHeartMother.Rating);
            Assert.AreEqual(123456, atomHeartMother.Length);
            Assert.AreEqual(LengthType.LP, atomHeartMother.LengthType);
            Assert.NotNull(atomHeartMother.Artwork);
            Assert.AreEqual(2, atomHeartMother.Artwork.Count);
            Assert.AreEqual("c:\\images\\atom-heart-mother-front.jpg", atomHeartMother.Artwork[0].FilePath);
            Assert.AreEqual("c:\\images\\atom-heart-mother-back.jpg", atomHeartMother.Artwork[1].FilePath);
            Assert.NotNull(atomHeartMother.Tracks);
            Assert.AreEqual(5, atomHeartMother.Tracks.Count);
            Assert.AreEqual(1, atomHeartMother.Tracks[0].TrackNumber);
            Assert.AreEqual('A', atomHeartMother.Tracks[0].Side);
            Assert.NotNull(atomHeartMother.Tracks[0].Song);
            Assert.AreEqual("Atom Heart Mother Suite", atomHeartMother.Tracks[0].Song.Name);
            Assert.AreEqual(2, atomHeartMother.Tracks[1].TrackNumber);
            Assert.AreEqual('B', atomHeartMother.Tracks[1].Side);
            Assert.NotNull(atomHeartMother.Tracks[1].Song);
            Assert.AreEqual("If", atomHeartMother.Tracks[1].Song.Name);
            Assert.AreEqual(3, atomHeartMother.Tracks[2].TrackNumber);
            Assert.AreEqual('B', atomHeartMother.Tracks[2].Side);
            Assert.NotNull(atomHeartMother.Tracks[2].Song);
            Assert.AreEqual("Summer '68", atomHeartMother.Tracks[2].Song.Name);
            Assert.AreEqual(4, atomHeartMother.Tracks[3].TrackNumber);
            Assert.AreEqual('B', atomHeartMother.Tracks[3].Side);
            Assert.NotNull(atomHeartMother.Tracks[3].Song);
            Assert.AreEqual("Fat Old Sun", atomHeartMother.Tracks[3].Song.Name);
            Assert.AreEqual(5, atomHeartMother.Tracks[4].TrackNumber);
            Assert.AreEqual('B', atomHeartMother.Tracks[4].Side);
            Assert.NotNull(atomHeartMother.Tracks[4].Song);
            Assert.AreEqual("Alan's Psychedelic Breakfast", atomHeartMother.Tracks[4].Song.Name);
            Assert.NotNull(atomHeartMother.Credits);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);
            Assert.AreEqual("David Gilmour", atomHeartMother.Credits[0].Person.Name);
            Assert.NotNull(atomHeartMother.Credits[0].Roles);
            Assert.AreEqual(4, atomHeartMother.Credits[0].Roles.Count);
            Assert.AreEqual("Backing Vocals", atomHeartMother.Credits[0].Roles[0].Name);
            Assert.AreEqual("Lead Guitar", atomHeartMother.Credits[0].Roles[1].Name);
            Assert.AreEqual("Lead Vocals", atomHeartMother.Credits[0].Roles[2].Name);
            Assert.AreEqual("Rhythm Guitar", atomHeartMother.Credits[0].Roles[3].Name);
            Assert.AreEqual("Nick Mason", atomHeartMother.Credits[1].Person.Name);
            Assert.NotNull(atomHeartMother.Credits[1].Roles);
            Assert.AreEqual(1, atomHeartMother.Credits[1].Roles.Count);
            Assert.AreEqual("Drums", atomHeartMother.Credits[1].Roles[0].Name);
            Assert.AreEqual("Norman Smith", atomHeartMother.Credits[2].Person.Name);
            Assert.NotNull(atomHeartMother.Credits[2].Roles);
            Assert.AreEqual(1, atomHeartMother.Credits[2].Roles.Count);
            Assert.AreEqual("Producer", atomHeartMother.Credits[2].Roles[0].Name);
            Assert.AreEqual("Richard Wright", atomHeartMother.Credits[3].Person.Name);
            Assert.NotNull(atomHeartMother.Credits[3].Roles);
            Assert.AreEqual(1, atomHeartMother.Credits[3].Roles.Count);
            Assert.AreEqual("Keyboards", atomHeartMother.Credits[3].Roles[0].Name);
            Assert.AreEqual("Roger Waters", atomHeartMother.Credits[4].Person.Name);
            Assert.NotNull(atomHeartMother.Credits[4].Roles);
            Assert.AreEqual(3, atomHeartMother.Credits[4].Roles.Count);
            Assert.AreEqual("Backing Vocals", atomHeartMother.Credits[4].Roles[0].Name);
            Assert.AreEqual("Bass Guitar", atomHeartMother.Credits[4].Roles[1].Name);
            Assert.AreEqual("Lead Vocals", atomHeartMother.Credits[4].Roles[2].Name);

            var sgtPepper = _albumRepository.GetAlbum(_dataSource, _sgtPepperId);
            Assert.NotNull(sgtPepper);
            Assert.AreEqual(_sgtPepperId, sgtPepper.Id);
            Assert.AreEqual("Sgt. Pepper's Lonely Hearts Club Band", sgtPepper.Name);
            Assert.AreEqual(1, sgtPepper.DiscCount);
            Assert.AreEqual("13", sgtPepper.TrackCounts);
            Assert.AreEqual(DateTime.Parse("6/1/1967"), sgtPepper.FirstReleasedOn);
            Assert.NotNull(sgtPepper.Artist);
            Assert.AreEqual(_beatlesId, sgtPepper.ArtistId);
            Assert.NotNull(sgtPepper.Artwork);
            Assert.AreEqual(3, sgtPepper.Artwork.Count);
            Assert.NotNull(sgtPepper.Tracks);
            Assert.AreEqual(13, sgtPepper.Tracks.Count);
            Assert.NotNull(sgtPepper.Media);
            Assert.AreEqual(3, sgtPepper.Media.Count);
            Assert.AreEqual(MediaTypes.CD | MediaTypes.DVD, sgtPepper.Media[0].MediaType);
            Assert.IsTrue(sgtPepper.Media[0].MediaType.HasFlag(MediaTypes.CD));
            Assert.IsTrue(sgtPepper.Media[0].MediaType.HasFlag(MediaTypes.DVD));
            Assert.IsFalse(sgtPepper.Media[0].MediaType.HasFlag(MediaTypes.BluRay));
            Assert.AreEqual(MediaTypes.Vinyl, sgtPepper.Media[1].MediaType);
            Assert.AreEqual(Condition.GoodPlus, sgtPepper.Media[1].MediaCondition);
            Assert.AreEqual(Condition.Good, sgtPepper.Media[1].PackagingCondition);
            Assert.AreEqual(DateTime.Parse("5/5/1985"), sgtPepper.Media[1].PurchasedOn);
            Assert.AreEqual(10.95m, sgtPepper.Media[1].PurchasedPrice);
            Assert.AreEqual(15.99m, sgtPepper.Media[1].ApproximateValue);
            Assert.AreEqual(_friendId, sgtPepper.Media[1].OnLoanToPersonId);
            Assert.NotNull(sgtPepper.Media[1].Release);
            Assert.AreEqual(DateTime.Parse("6/1/1967"), sgtPepper.Media[1].Release.ReleaseDate);
            Assert.AreEqual(_capitolId, sgtPepper.Media[1].Release.LabelId);
            Assert.AreEqual(MediaTypes.Vinyl, sgtPepper.Media[1].Release.MediaTypes);
            Assert.AreEqual("SMAS 2653", sgtPepper.Media[1].Release.CatalogNumber);
            Assert.AreEqual(MediaTypes.Digital, sgtPepper.Media[2].MediaType);
            Assert.AreEqual(DigitalFormats.M4A, sgtPepper.Media[2].DigitalFormat);
            Assert.NotNull(sgtPepper.Media[2].Release);
            Assert.AreEqual(DateTime.Parse("11/16/2010"), sgtPepper.Media[2].Release.ReleaseDate);

            var whiteAlbum = _albumRepository.GetAlbum(_dataSource, _whiteAlbumId);
            Assert.NotNull(whiteAlbum);
            Assert.AreEqual(_whiteAlbumId, whiteAlbum.Id);
            Assert.AreEqual("The Beatles", whiteAlbum.Name);
            Assert.AreEqual(_beatlesId, whiteAlbum.ArtistId);
            Assert.NotNull(whiteAlbum.Artist);
            Assert.AreEqual("The Beatles", whiteAlbum.Artist.Name);
            Assert.NotNull(whiteAlbum.Tracks);
            Assert.AreEqual(4, whiteAlbum.Tracks.Count);
            Assert.AreEqual(1, whiteAlbum.Tracks[0].TrackNumber);
            Assert.AreEqual(1, whiteAlbum.Tracks[0].DiscNumber);
            Assert.AreEqual('1', whiteAlbum.Tracks[0].Side);
            Assert.NotNull(whiteAlbum.Tracks[0].Song);
            Assert.AreEqual("Back in the USSR", whiteAlbum.Tracks[0].Song.Name);
            Assert.AreEqual(9, whiteAlbum.Tracks[1].TrackNumber);
            Assert.AreEqual(1, whiteAlbum.Tracks[1].DiscNumber);
            Assert.AreEqual('2', whiteAlbum.Tracks[1].Side);
            Assert.NotNull(whiteAlbum.Tracks[1].Song);
            Assert.AreEqual("Martha My Dear", whiteAlbum.Tracks[1].Song.Name);
            Assert.AreEqual(1, whiteAlbum.Tracks[2].TrackNumber);
            Assert.AreEqual(2, whiteAlbum.Tracks[2].DiscNumber);
            Assert.AreEqual('3', whiteAlbum.Tracks[2].Side);
            Assert.NotNull(whiteAlbum.Tracks[2].Song);
            Assert.AreEqual("Birthday", whiteAlbum.Tracks[2].Song.Name);
            Assert.AreEqual(8, whiteAlbum.Tracks[3].TrackNumber);
            Assert.AreEqual(2, whiteAlbum.Tracks[3].DiscNumber);
            Assert.AreEqual('4', whiteAlbum.Tracks[3].Side);
            Assert.NotNull(whiteAlbum.Tracks[3].Song);
            Assert.AreEqual("Revolution 1", whiteAlbum.Tracks[3].Song.Name);

            var helpFromMyFwends = _albumRepository.GetAlbum(_dataSource, _helpFromMyFwendsId);
            Assert.NotNull(helpFromMyFwends);
            Assert.AreEqual(_helpFromMyFwendsId, helpFromMyFwends.Id);
            Assert.AreEqual("With a Little Help from My Fwends", helpFromMyFwends.Name);
            Assert.AreEqual(_flamingLipsId, helpFromMyFwends.ArtistId);
            Assert.NotNull(helpFromMyFwends.Artist);
            Assert.AreEqual("The Flaming Lips", helpFromMyFwends.Artist.Name);
            Assert.IsTrue(helpFromMyFwends.IsTribute);
            Assert.AreEqual(_beatlesId, helpFromMyFwends.TributeArtistId);
            Assert.AreEqual(_sgtPepperId, helpFromMyFwends.OriginalAlbumId);
            Assert.NotNull(helpFromMyFwends.Collaborators);
            Assert.AreEqual(3, helpFromMyFwends.Collaborators.Count);
            Assert.AreEqual("Phantogram", helpFromMyFwends.Collaborators[0].Artist.Name);
            Assert.IsTrue(helpFromMyFwends.Collaborators[0].IsFeatured);
            Assert.AreEqual("Moby", helpFromMyFwends.Collaborators[1].Artist.Name);
            Assert.AreEqual("My Morning Jacket", helpFromMyFwends.Collaborators[2].Artist.Name);

            var pulpFictionSoundtrack = _albumRepository.GetAlbum(_dataSource, _pulpFictionSoundtrackId);
            Assert.NotNull(pulpFictionSoundtrack);
            Assert.IsTrue(pulpFictionSoundtrack.IsSoundtrack);
            Assert.IsTrue(pulpFictionSoundtrack.IsCompilation);
            Assert.IsTrue(pulpFictionSoundtrack.IsMixedArtist);
            Assert.IsNull(pulpFictionSoundtrack.ArtistId);
            Assert.IsNull(pulpFictionSoundtrack.Artist);
            Assert.AreEqual(16, pulpFictionSoundtrack.Tracks.Count);
            Assert.AreEqual("Misirlou", pulpFictionSoundtrack.Tracks[0].Song.Name);
            Assert.AreEqual("Dick Dale & His Del-Tones", pulpFictionSoundtrack.Tracks[0].Song.Artist.Name);
            Assert.AreEqual("Royale with Cheese", pulpFictionSoundtrack.Tracks[1].Song.Name);
            Assert.AreEqual("John Travolta and Samuel L. Jackson", pulpFictionSoundtrack.Tracks[1].Song.Artist.Name);
            Assert.AreEqual("Jungle Boogie", pulpFictionSoundtrack.Tracks[2].Song.Name);
            Assert.AreEqual("Kool & The Gang", pulpFictionSoundtrack.Tracks[2].Song.Artist.Name);
            Assert.AreEqual("Let's Stay Together", pulpFictionSoundtrack.Tracks[3].Song.Name);
            Assert.AreEqual("Al Green", pulpFictionSoundtrack.Tracks[3].Song.Artist.Name);
            Assert.AreEqual("Bustin' Surfboards", pulpFictionSoundtrack.Tracks[4].Song.Name);
            Assert.AreEqual("The Tornados", pulpFictionSoundtrack.Tracks[4].Song.Artist.Name);
            Assert.AreEqual("Lonesome Town", pulpFictionSoundtrack.Tracks[5].Song.Name);
            Assert.AreEqual("Ricky Nelson", pulpFictionSoundtrack.Tracks[5].Song.Artist.Name);
            Assert.AreEqual("Son of a Preacher Man", pulpFictionSoundtrack.Tracks[6].Song.Name);
            Assert.AreEqual("Dusty Springfield", pulpFictionSoundtrack.Tracks[6].Song.Artist.Name);
            Assert.AreEqual("Bullwinkle Part II", pulpFictionSoundtrack.Tracks[7].Song.Name);
            Assert.AreEqual("The Centurians", pulpFictionSoundtrack.Tracks[7].Song.Artist.Name);
            Assert.AreEqual("You Never Can Tell", pulpFictionSoundtrack.Tracks[8].Song.Name);
            Assert.AreEqual("Chuck Berry", pulpFictionSoundtrack.Tracks[8].Song.Artist.Name);
            Assert.AreEqual("Girl, You'll Be A Woman Soon", pulpFictionSoundtrack.Tracks[9].Song.Name);
            Assert.AreEqual("Urge Overkill", pulpFictionSoundtrack.Tracks[9].Song.Artist.Name);
            Assert.AreEqual(_girlYoullBeAWomanSoonId, pulpFictionSoundtrack.Tracks[9].Song.OriginalSongId);
            Assert.AreEqual("If Love Is A Red Dress (Hang Me In Rags)", pulpFictionSoundtrack.Tracks[10].Song.Name);
            Assert.AreEqual("Maria McKee", pulpFictionSoundtrack.Tracks[10].Song.Artist.Name);
            Assert.AreEqual("Comanche", pulpFictionSoundtrack.Tracks[11].Song.Name);
            Assert.AreEqual("The Revels", pulpFictionSoundtrack.Tracks[11].Song.Artist.Name);
            Assert.AreEqual("Flowers on the Wall", pulpFictionSoundtrack.Tracks[12].Song.Name);
            Assert.AreEqual("The Statler Brothers", pulpFictionSoundtrack.Tracks[12].Song.Artist.Name);
            Assert.AreEqual("Personality Goes a Long Way", pulpFictionSoundtrack.Tracks[13].Song.Name);
            Assert.AreEqual("John Travolta and Samuel L. Jackson", pulpFictionSoundtrack.Tracks[13].Song.Artist.Name);
            Assert.AreEqual("Surf Rider", pulpFictionSoundtrack.Tracks[14].Song.Name);
            Assert.AreEqual("The Lively Ones", pulpFictionSoundtrack.Tracks[14].Song.Artist.Name);
            Assert.AreEqual("Ezekiel 25:17", pulpFictionSoundtrack.Tracks[15].Song.Name);
            Assert.AreEqual("Samuel L. Jackson", pulpFictionSoundtrack.Tracks[15].Song.Artist.Name);

            var beachBoysChristmas = _albumRepository.GetAlbum(_dataSource, _beachBoysChristmasId);
            Assert.NotNull(beachBoysChristmas);
            Assert.AreEqual(_beachBoysChristmasId, beachBoysChristmas.Id);
            Assert.AreEqual("The Beach Boys' Christmas Album", beachBoysChristmas.Name);
            Assert.NotNull(beachBoysChristmas.Artist);
            Assert.AreEqual("The Beach Boys", beachBoysChristmas.Artist.Name);
            Assert.IsTrue(beachBoysChristmas.IsHoliday);
            Assert.NotNull(beachBoysChristmas.Holiday);
            Assert.AreEqual(_christmasId, beachBoysChristmas.Holiday.Id);
            Assert.AreEqual("Christmas", beachBoysChristmas.Holiday.Name);
            Assert.AreEqual(1, beachBoysChristmas.Holiday.StartDay);
            Assert.AreEqual(Month.December, beachBoysChristmas.Holiday.StartMonth);
            Assert.AreEqual(26, beachBoysChristmas.Holiday.EndDay);
            Assert.AreEqual(Month.December, beachBoysChristmas.Holiday.EndMonth);

            var yearZeroRemixed = _albumRepository.GetAlbum(_dataSource, _yearZeroRemixedId);
            Assert.NotNull(yearZeroRemixed);
            Assert.AreEqual(_yearZeroRemixedId, yearZeroRemixed.Id);
            Assert.AreEqual("Y34RZ3R0R3M1X3D", yearZeroRemixed.Name);
            Assert.NotNull(yearZeroRemixed.Artist);
            Assert.AreEqual("Nine Inch Nails", yearZeroRemixed.Artist.Name);
            Assert.IsTrue(yearZeroRemixed.IsRemix);
            Assert.IsTrue(yearZeroRemixed.IsMixedArtist);
            Assert.AreEqual(_yearZeroId, yearZeroRemixed.OriginalAlbumId);

            var bandOfGypsys = _albumRepository.GetAlbum(_dataSource, _bandOfGypsysId);
            Assert.NotNull(bandOfGypsys);
            Assert.AreEqual(_bandOfGypsysId, bandOfGypsys.Id);
            Assert.AreEqual("Band of Gypsys", bandOfGypsys.Name);
            Assert.NotNull(bandOfGypsys.Artist);
            Assert.AreEqual("Jimi Hendrix", bandOfGypsys.Artist.Name);
            Assert.IsTrue(bandOfGypsys.IsLive);

            var letDown = _albumRepository.GetAlbum(_dataSource, _letDownId);
            Assert.NotNull(letDown);
            Assert.AreEqual(_letDownId, letDown.Id);
            Assert.AreEqual("Let Down", letDown.Name);
            Assert.NotNull(letDown.Artist);
            Assert.AreEqual("Radiohead", letDown.Artist.Name);
            Assert.AreEqual("DJ Promo", letDown.Edition);
            Assert.AreEqual(LengthType.Single, letDown.LengthType);
            Assert.IsTrue(letDown.IsPromotional);
            Assert.IsTrue(letDown.IsBootleg);

            var globalUnderground7 = _albumRepository.GetAlbum(_dataSource, _globalUnderground7Id);
            Assert.NotNull(globalUnderground7);
            Assert.AreEqual(_globalUnderground7Id, globalUnderground7.Id);
            Assert.AreEqual("Global Underground 007: New York", globalUnderground7.Name);
            Assert.IsTrue(globalUnderground7.IsCompilation);
            Assert.IsTrue(globalUnderground7.IsMixedArtist);
            Assert.IsTrue(globalUnderground7.IsSingleTrack);
            Assert.NotNull(globalUnderground7.Tracks);
            Assert.AreEqual(3, globalUnderground7.Tracks.Count);
            Assert.AreEqual(1, globalUnderground7.Tracks[0].TrackNumber);
            Assert.AreEqual("Bliss", globalUnderground7.Tracks[0].Song.Name);
            Assert.AreEqual("Mystica Mix", globalUnderground7.Tracks[0].Song.Version);
            Assert.AreEqual("Mystica", globalUnderground7.Tracks[0].Song.Artist.Name);
            Assert.AreEqual(0, globalUnderground7.Tracks[0].StartTime);
            Assert.AreEqual(355000, globalUnderground7.Tracks[0].StopTime);
            Assert.AreEqual(2, globalUnderground7.Tracks[1].TrackNumber);
            Assert.AreEqual("Rescue Me", globalUnderground7.Tracks[1].Song.Name);
            Assert.AreEqual("Jamie Myerson", globalUnderground7.Tracks[1].Song.Artist.Name);
            Assert.AreEqual(355000, globalUnderground7.Tracks[1].StartTime);
            Assert.AreEqual(583000, globalUnderground7.Tracks[1].StopTime);
            Assert.AreEqual(3, globalUnderground7.Tracks[2].TrackNumber);
            Assert.AreEqual("Summersault", globalUnderground7.Tracks[2].Song.Name);
            Assert.AreEqual("Taste Experience", globalUnderground7.Tracks[2].Song.Artist.Name);
            Assert.AreEqual(583000, globalUnderground7.Tracks[2].StartTime);
            Assert.AreEqual(1025000, globalUnderground7.Tracks[2].StopTime);
        }

        /// <summary>
        /// Tests the get album release method.
        /// </summary>
        [Test]
        public void GetAlbumReleaseTest()
        {
            var helpFromMyFwendsRelease = _albumRepository.GetAlbumRelease(_dataSource, _helpFromMyFwendsReleaseId);
            Assert.NotNull(helpFromMyFwendsRelease);
            Assert.AreEqual(DateTime.Parse("10/28/14"), helpFromMyFwendsRelease.ReleaseDate);
            Assert.AreEqual("544000-1", helpFromMyFwendsRelease.CatalogNumber);
            Assert.AreEqual(_warnerBrothersId, helpFromMyFwendsRelease.LabelId);
            Assert.IsTrue(helpFromMyFwendsRelease.MediaTypes.HasFlag(MediaTypes.CD));
            Assert.IsTrue(helpFromMyFwendsRelease.MediaTypes.HasFlag(MediaTypes.Vinyl));
            Assert.IsTrue(helpFromMyFwendsRelease.MediaTypes.HasFlag(MediaTypes.Digital));
            Assert.IsFalse(helpFromMyFwendsRelease.MediaTypes.HasFlag(MediaTypes.Cassette));
            Assert.IsNotNull(helpFromMyFwendsRelease.DigitalFormats);
            Assert.IsTrue(helpFromMyFwendsRelease.DigitalFormats != null && helpFromMyFwendsRelease.DigitalFormats.Value.HasFlag(DigitalFormats.MP3));
            Assert.IsTrue(helpFromMyFwendsRelease.DigitalFormats.Value.HasFlag(DigitalFormats.WAV));
            Assert.IsFalse(helpFromMyFwendsRelease.DigitalFormats.Value.HasFlag(DigitalFormats.M4A));
        }

        /// <summary>
        /// Tests the list albums method.
        /// </summary>
        [Test]
        public void ListAlbumsTest()
        {
            var albums = _albumRepository.ListAlbums(_dataSource);
            Assert.NotNull(albums);
            Assert.AreEqual(13, albums.Count);
            Assert.AreEqual(_bandOfGypsysId, albums[0].Id);
            Assert.AreEqual("Band of Gypsys", albums[0].Name);
            Assert.AreEqual("Jimi Hendrix", albums[0].Artist.Name);
            Assert.IsTrue(albums[0].IsLive);
            Assert.AreEqual(_yearZeroRemixedId, albums[1].Id);
            Assert.AreEqual("Y34RZ3R0R3M1X3D", albums[1].Name);
            Assert.AreEqual("Nine Inch Nails", albums[1].Artist.Name);
            Assert.IsTrue(albums[1].IsRemix);
            Assert.IsTrue(albums[1].IsMixedArtist);
            Assert.AreEqual(_yearZeroId, albums[1].OriginalAlbumId);
            Assert.AreEqual(_yearZeroId, albums[2].Id);
            Assert.AreEqual("Year Zero", albums[2].Name);
            Assert.AreEqual("Nine Inch Nails", albums[2].Artist.Name);
            Assert.AreEqual(_globalUnderground7Id, albums[3].Id);
            Assert.AreEqual("Global Underground 007: New York", albums[3].Name);
            Assert.AreEqual("Paul Oakenfold", albums[3].Artist.Name);
            Assert.IsTrue(albums[3].IsMixedArtist);
            Assert.IsTrue(albums[3].IsCompilation);
            Assert.IsTrue(albums[3].IsSingleTrack);
            Assert.AreEqual(_meddleId, albums[4].Id);
            Assert.AreEqual("Meddle", albums[4].Name);
            Assert.AreEqual("Pink Floyd", albums[4].Artist.Name);
            Assert.AreEqual(_atomHeartMotherId, albums[5].Id);
            Assert.AreEqual("Atom Heart Mother", albums[5].Name);
            Assert.AreEqual("Pink Floyd", albums[5].Artist.Name);
            Assert.AreEqual(DateTime.Parse("2/10/1970"), albums[5].FirstReleasedOn);
            Assert.AreEqual("Atom Heart Mother Description", albums[5].Description);
            Assert.AreEqual("Atom Heart Mother Liner Notes", albums[5].LinerNotes);
            Assert.AreEqual(5, albums[5].Rating);
            Assert.AreEqual(123456, albums[5].Length);
            Assert.AreEqual(LengthType.LP, albums[5].LengthType);
            Assert.AreEqual(_letDownId, albums[6].Id);
            Assert.AreEqual("Let Down", albums[6].Name);
            Assert.AreEqual("DJ Promo", albums[6].Edition);
            Assert.AreEqual("Radiohead", albums[6].Artist.Name);
            Assert.IsTrue(albums[6].IsPromotional);
            Assert.IsTrue(albums[6].IsBootleg);
            Assert.AreEqual(LengthType.Single, albums[6].LengthType);
            Assert.AreEqual(_beachBoysChristmasId, albums[7].Id);
            Assert.AreEqual("The Beach Boys' Christmas Album", albums[7].Name);
            Assert.AreEqual("The Beach Boys", albums[7].Artist.Name);
            Assert.IsTrue(albums[7].IsHoliday);
            Assert.AreEqual("Christmas", albums[7].Holiday.Name);
            Assert.AreEqual(1, albums[7].Holiday.StartDay);
            Assert.AreEqual(Month.December, albums[7].Holiday.StartMonth);
            Assert.AreEqual(26, albums[7].Holiday.EndDay);
            Assert.AreEqual(Month.December, albums[7].Holiday.EndMonth);
            Assert.AreEqual(_sgtPepperId, albums[8].Id);
            Assert.AreEqual("Sgt. Pepper's Lonely Hearts Club Band", albums[8].Name);
            Assert.AreEqual("The Beatles", albums[8].Artist.Name);
            Assert.AreEqual(_magicalMysteryTourId, albums[9].Id);
            Assert.AreEqual("Magical Mystery Tour", albums[9].Name);
            Assert.AreEqual("The Beatles", albums[9].Artist.Name);
            Assert.AreEqual(_whiteAlbumId, albums[10].Id);
            Assert.AreEqual("The Beatles", albums[10].Name);
            Assert.AreEqual("The White Album", albums[10].UnofficialName);
            Assert.AreEqual("The Beatles", albums[10].Artist.Name);
            Assert.AreEqual(_helpFromMyFwendsId, albums[11].Id);
            Assert.AreEqual("With a Little Help from My Fwends", albums[11].Name);
            Assert.AreEqual("The Flaming Lips", albums[11].Artist.Name);
            Assert.IsTrue(albums[11].IsTribute);
            Assert.AreEqual(_beatlesId, albums[11].TributeArtistId);
            Assert.AreEqual(_sgtPepperId, albums[11].OriginalAlbumId);
            Assert.AreEqual(_pulpFictionSoundtrackId, albums[12].Id);
            Assert.AreEqual("Pulp Fiction Original Soundtrack", albums[12].Name);
            Assert.IsNull(albums[12].ArtistId);
            Assert.IsNull(albums[12].Artist);
            Assert.IsTrue(albums[12].IsSoundtrack);
            Assert.IsTrue(albums[12].IsCompilation);
            Assert.IsTrue(albums[12].IsMixedArtist);
        }

        /// <summary>
        /// Tests the list artist albums method.
        /// </summary>
        [Test]
        public void ListArtistAlbumsTest()
        {
            var albums = _albumRepository.ListArtistAlbums(_dataSource, _beatlesId);
            Assert.NotNull(albums);
            Assert.AreEqual(3, albums.Count);
            Assert.AreEqual(_sgtPepperId, albums[0].Id);
            Assert.AreEqual("Sgt. Pepper's Lonely Hearts Club Band", albums[0].Name);
            Assert.AreEqual("The Beatles", albums[0].Artist.Name);
            Assert.AreEqual(_magicalMysteryTourId, albums[1].Id);
            Assert.AreEqual("Magical Mystery Tour", albums[1].Name);
            Assert.AreEqual("The Beatles", albums[1].Artist.Name);
            Assert.AreEqual(_whiteAlbumId, albums[2].Id);
            Assert.AreEqual("The Beatles", albums[2].Name);
            Assert.AreEqual("The White Album", albums[2].UnofficialName);
            Assert.AreEqual("The Beatles", albums[2].Artist.Name);
        }

        /// <summary>
        /// Tests the list album releases method.
        /// </summary>
        [Test]
        public void ListAlbumReleasesTest()
        {
            var magicalMysteryTour = _albumRepository.GetAlbum(_dataSource, _magicalMysteryTourId);
            Assert.NotNull(magicalMysteryTour);
            Assert.AreEqual(DateTime.Parse("11/19/1967"), magicalMysteryTour.FirstReleasedOn);
            var magicalMysteryTourReleases = _albumRepository.ListAlbumReleases(_dataSource, _magicalMysteryTourId);
            Assert.NotNull(magicalMysteryTourReleases);
            Assert.AreEqual(3, magicalMysteryTourReleases.Count);
            Assert.AreEqual(DateTime.Parse("11/19/1967"), magicalMysteryTourReleases[0].ReleaseDate);
            Assert.AreEqual("MAL 2835", magicalMysteryTourReleases[0].CatalogNumber);
            Assert.AreEqual(_capitolId, magicalMysteryTourReleases[0].LabelId);
            Assert.AreEqual(DateTime.Parse("11/27/1967"), magicalMysteryTourReleases[1].ReleaseDate);
            Assert.AreEqual("SMAL 2835", magicalMysteryTourReleases[1].CatalogNumber);
            Assert.AreEqual(_capitolId, magicalMysteryTourReleases[1].LabelId);
            Assert.AreEqual(DateTime.Parse("12/8/1967"), magicalMysteryTourReleases[2].ReleaseDate);
            Assert.AreEqual("SMAL-2835", magicalMysteryTourReleases[2].CatalogNumber);
            Assert.AreEqual(_capitolId, magicalMysteryTourReleases[2].LabelId);
        }

        /// <summary>
        /// Tests the delete album track method.
        /// </summary>
        [Test]
        public void DeleteAlbumTrackTest()
        {
            var whiteAlbum = _albumRepository.GetAlbum(_dataSource, _whiteAlbumId);
            Assert.NotNull(whiteAlbum);
            Assert.AreEqual(4, whiteAlbum.Tracks.Count);
            var glassOnion = Song.Create("Glass Onion", whiteAlbum.Artist);
            _songRepository.AddSong(_dataSource, glassOnion);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(whiteAlbum, glassOnion, 3, 1, '1'));

            whiteAlbum = _albumRepository.GetAlbum(_dataSource, _whiteAlbumId);
            Assert.AreEqual(5, whiteAlbum.Tracks.Count);
            Assert.AreEqual("Glass Onion", whiteAlbum.Tracks[1].Song.Name);
            _albumRepository.DeleteAlbumTrack(_dataSource, whiteAlbum.Tracks[1]);

            whiteAlbum = _albumRepository.GetAlbum(_dataSource, _whiteAlbumId);
            Assert.AreEqual(4, whiteAlbum.Tracks.Count);
            Assert.AreEqual("Martha My Dear", whiteAlbum.Tracks[1].Song.Name);
        }

        /// <summary>
        /// Tests the delete album media method.
        /// </summary>
        [Test]
        public void DeleteAlbumMediaTest()
        {
            var capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            Assert.NotNull(capitol);
            var sgtPepper = _albumRepository.GetAlbum(_dataSource, _sgtPepperId);
            Assert.NotNull(sgtPepper);
            Assert.AreEqual(3, sgtPepper.Media.Count);
            var sgtPepperRelease4 = AlbumRelease.Create(sgtPepper, DateTime.Parse("11/16/1967"), MediaTypes.EightTrack, capitol, "8XT 2653");
            _albumRepository.AddAlbumRelease(_dataSource, sgtPepperRelease4);
            var sgtPepperMedia4 = AlbumMedia.Create(sgtPepper, MediaTypes.EightTrack);
            sgtPepperMedia4.Release = sgtPepperRelease4;
            _albumRepository.AddAlbumMedia(_dataSource, sgtPepperMedia4);

            sgtPepper = _albumRepository.GetAlbum(_dataSource, _sgtPepperId);
            Assert.NotNull(sgtPepper);
            Assert.AreEqual(4, sgtPepper.Media.Count);
            Assert.AreEqual(MediaTypes.EightTrack, sgtPepper.Media[2].MediaType);
            _albumRepository.DeleteAlbumMedia(_dataSource, sgtPepper.Media[2]);

            sgtPepper = _albumRepository.GetAlbum(_dataSource, _sgtPepperId);
            Assert.NotNull(sgtPepper);
            Assert.AreEqual(3, sgtPepper.Media.Count);
            Assert.AreEqual(MediaTypes.Digital, sgtPepper.Media[2].MediaType);
        }

        /// <summary>
        /// Tests the delete album artwork method.
        /// </summary>
        [Test]
        public void DeleteAlbumArtworkTest()
        {
            var atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.NotNull(atomHeartMother.Artwork);
            Assert.AreEqual(2, atomHeartMother.Artwork.Count);
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(atomHeartMother, "c:\\images\\atom-heart-mother-gatefold.jpg", 3));

            atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.NotNull(atomHeartMother.Artwork);
            Assert.AreEqual(3, atomHeartMother.Artwork.Count);
            Assert.AreEqual("c:\\images\\atom-heart-mother-gatefold.jpg", atomHeartMother.Artwork[2].FilePath);
            _albumRepository.DeleteAlbumArtwork(_dataSource, atomHeartMother.Artwork[2]);

            atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.NotNull(atomHeartMother.Artwork);
            Assert.AreEqual(2, atomHeartMother.Artwork.Count);
            Assert.AreEqual("c:\\images\\atom-heart-mother-front.jpg", atomHeartMother.Artwork[0].FilePath);
            Assert.AreEqual("c:\\images\\atom-heart-mother-back.jpg", atomHeartMother.Artwork[1].FilePath);
        }

        /// <summary>
        /// Tests the delete album release method.
        /// </summary>
        [Test]
        public void DeleteAlbumReleaseTest()
        {
            var magicalMysteryTour = _albumRepository.GetAlbum(_dataSource, _magicalMysteryTourId);
            Assert.NotNull(magicalMysteryTour);
            var magicalMysteryTourReleases = _albumRepository.ListAlbumReleases(_dataSource, _magicalMysteryTourId);
            Assert.NotNull(magicalMysteryTourReleases);
            Assert.AreEqual(3, magicalMysteryTourReleases.Count);

            var magicalMysteryTourRelease4 = AlbumRelease.Create(magicalMysteryTour, DateTime.Parse("1/8/1968"), MediaTypes.Vinyl);
            magicalMysteryTourRelease4.LabelId = _capitolId;
            magicalMysteryTourRelease4.CatalogNumber = "ADDED";
            _albumRepository.AddAlbumRelease(_dataSource, magicalMysteryTourRelease4);

            magicalMysteryTourReleases = _albumRepository.ListAlbumReleases(_dataSource, _magicalMysteryTourId);
            Assert.NotNull(magicalMysteryTourReleases);
            Assert.AreEqual(4, magicalMysteryTourReleases.Count);
            Assert.AreEqual("ADDED", magicalMysteryTourReleases[3].CatalogNumber);
            _albumRepository.DeleteAlbumRelease(_dataSource, magicalMysteryTourReleases[3]);

            magicalMysteryTourReleases = _albumRepository.ListAlbumReleases(_dataSource, _magicalMysteryTourId);
            Assert.NotNull(magicalMysteryTourReleases);
            Assert.AreEqual(3, magicalMysteryTourReleases.Count);
        }

        /// <summary>
        /// Tests the delete album collaborator method.
        /// </summary>
        [Test]
        public void DeleteAlbumCollaboratorTest()
        {
            var helpFromMyFwends = _albumRepository.GetAlbum(_dataSource, _helpFromMyFwendsId);
            Assert.NotNull(helpFromMyFwends);
            Assert.NotNull(helpFromMyFwends.Collaborators);
            Assert.AreEqual(3, helpFromMyFwends.Collaborators.Count);

            var mileyCyrus = Artist.Create("Miley Cyrus");
            _artistRepository.AddArtist(_dataSource, mileyCyrus);
            _albumRepository.AddAlbumCollaborator(_dataSource, AlbumCollaborator.Create(helpFromMyFwends, mileyCyrus));

            helpFromMyFwends = _albumRepository.GetAlbum(_dataSource, _helpFromMyFwendsId);
            Assert.AreEqual(4, helpFromMyFwends.Collaborators.Count);
            Assert.AreEqual(mileyCyrus.Id, helpFromMyFwends.Collaborators[1].ArtistId);
            _albumRepository.DeleteAlbumCollaborator(_dataSource, helpFromMyFwends.Collaborators[1]);

            helpFromMyFwends = _albumRepository.GetAlbum(_dataSource, _helpFromMyFwendsId);
            Assert.AreEqual(3, helpFromMyFwends.Collaborators.Count);
            Assert.AreEqual("Moby", helpFromMyFwends.Collaborators[1].Artist.Name);
        }

        /// <summary>
        /// Tests the delete album credit role method.
        /// </summary>
        [Test]
        public void DeleteAlbumCreditRoleTest()
        {
            var leadVocals = _roleRepository.GetRole(_dataSource, "Lead Vocals");
            Assert.NotNull(leadVocals);
            var atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.NotNull(atomHeartMother.Credits);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);

            var normanCredit = atomHeartMother.Credits.SingleOrDefault(c => c.Person.Name == "Norman Smith");
            Assert.NotNull(normanCredit);
            Assert.AreEqual(1, normanCredit.Roles.Count);
            _albumRepository.AddAlbumCreditRole(_dataSource, normanCredit, leadVocals);

            atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            normanCredit = atomHeartMother.Credits.SingleOrDefault(c => c.Person.Name == "Norman Smith");
            Assert.NotNull(normanCredit);
            Assert.AreEqual(2, normanCredit.Roles.Count);
            Assert.AreEqual("Lead Vocals", normanCredit.Roles[0].Name);
            _albumRepository.DeleteAlbumCreditRole(_dataSource, normanCredit, leadVocals);

            atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            normanCredit = atomHeartMother.Credits.SingleOrDefault(c => c.Person.Name == "Norman Smith");
            Assert.NotNull(normanCredit);
            Assert.AreEqual(1, normanCredit.Roles.Count);
            Assert.AreEqual("Producer", normanCredit.Roles[0].Name);
        }

        /// <summary>
        /// Tests the delete album credit method.
        /// </summary>
        [Test]
        public void DeleteAlbumCreditTest()
        {
            var leadVocals = _roleRepository.GetRole(_dataSource, "Lead Vocals");
            Assert.NotNull(leadVocals);
            var leadGuitar = _roleRepository.GetRole(_dataSource, "Lead Guitar");
            Assert.NotNull(leadGuitar);
            var pinkFloyd = _artistRepository.GetArtist(_dataSource, _pinkFloydId);
            Assert.NotNull(pinkFloyd);

            var atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.NotNull(atomHeartMother);
            Assert.AreEqual(pinkFloyd.Id, atomHeartMother.ArtistId);
            Assert.NotNull(atomHeartMother.Credits);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);

            var sydBarett = Person.Create("Syd Barett");
            _personRepository.AddPerson(_dataSource, sydBarett);
            var sydAlbumCredit = AlbumCredit.Create(atomHeartMother, sydBarett);
            _albumRepository.AddAlbumCredit(_dataSource, sydAlbumCredit);
            _albumRepository.AddAlbumCreditRole(_dataSource, sydAlbumCredit, leadGuitar);
            _albumRepository.AddAlbumCreditRole(_dataSource, sydAlbumCredit, leadVocals);

            atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.AreEqual(6, atomHeartMother.Credits.Count);
            Assert.AreEqual("Syd Barett", atomHeartMother.Credits[5].Person.Name);
            _albumRepository.DeleteAlbumCredit(_dataSource, atomHeartMother.Credits[5]);

            atomHeartMother = _albumRepository.GetAlbum(_dataSource, _atomHeartMotherId);
            Assert.AreEqual(5, atomHeartMother.Credits.Count);
        }

        /// <summary>
        /// Tests the delete album method.
        /// </summary>
        [Test]
        public void DeleteAlbumTest()
        {
            var harvest = Label.Create("Harvest");
            _labelRepository.AddLabel(_dataSource, harvest);

            var composer = Role.Create("Composer");
            _roleRepository.AddRole(_dataSource, composer);

            var pinkFloyd = _artistRepository.GetArtist(_dataSource, _pinkFloydId);
            Assert.NotNull(pinkFloyd);
            Assert.NotNull(pinkFloyd.Members);
            Assert.AreEqual(4, pinkFloyd.Members.Count);
            var rogerMember = pinkFloyd.Members[0];
            Assert.AreEqual("Roger Waters", rogerMember.Person.Name);
            var davidMember = pinkFloyd.Members[1];
            Assert.AreEqual("David Gilmour", davidMember.Person.Name);
            var nickMember = pinkFloyd.Members[2];
            Assert.AreEqual("Nick Mason", nickMember.Person.Name);
            var richardMember = pinkFloyd.Members[3];
            Assert.AreEqual("Richard Wright", richardMember.Person.Name);

            var darkSideOfTheMoon = Album.Create("The Dark Side of the Moon", pinkFloyd);
            var darkSideOfTheMoonId = darkSideOfTheMoon.Id;
            darkSideOfTheMoon.FirstReleasedOn = DateTime.Parse("3/1/1973");
            darkSideOfTheMoon.Description = "The Dark Side of the Moon Description";
            darkSideOfTheMoon.LinerNotes = "The Dark Side of the Moon Liner Notes";
            darkSideOfTheMoon.Length = 98765;
            darkSideOfTheMoon.LengthType = LengthType.LP;
            _albumRepository.AddAlbum(_dataSource, darkSideOfTheMoon);
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(darkSideOfTheMoon, "c:\\images\\the-dark-side-of-the-moon-front.jpg", 1));
            _albumRepository.AddAlbumArtwork(_dataSource, AlbumArtwork.Create(darkSideOfTheMoon, "c:\\images\\the-dark-side-of-the-moon-back.jpg", 2));
            var speakToMe = Song.Create("Speak to Me", pinkFloyd);
            _songRepository.AddSong(_dataSource, speakToMe);
            var nickSongCredit = SongCredit.Create(speakToMe, nickMember.Person);
            _songRepository.AddSongCredit(_dataSource, nickSongCredit);
            _songRepository.AddSongCreditRole(_dataSource, nickSongCredit, composer);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, speakToMe, 1, 'A'));
            var breathe = Song.Create("Breathe", pinkFloyd);
            _songRepository.AddSong(_dataSource, breathe);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, breathe, 2, 'A'));
            var onTheRun = Song.Create("On the Run", pinkFloyd);
            _songRepository.AddSong(_dataSource, onTheRun);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, onTheRun, 3, 'A'));
            var time = Song.Create("Time", pinkFloyd);
            _songRepository.AddSong(_dataSource, time);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, time, 4, 'A'));
            var greatGigInTheSky = Song.Create("The Great Gig in the Sky", pinkFloyd);
            _songRepository.AddSong(_dataSource, greatGigInTheSky);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, greatGigInTheSky, 5, 'A'));
            var money = Song.Create("Money", pinkFloyd);
            _songRepository.AddSong(_dataSource, money);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, money, 6, 'B'));
            var usAndThem = Song.Create("Us and Them", pinkFloyd);
            _songRepository.AddSong(_dataSource, usAndThem);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, usAndThem, 7, 'B'));
            var anyColourYouLike = Song.Create("Any Colour You Like", pinkFloyd);
            _songRepository.AddSong(_dataSource, anyColourYouLike);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, anyColourYouLike, 8, 'B'));
            var brainDamage = Song.Create("Brain Damage", pinkFloyd);
            _songRepository.AddSong(_dataSource, brainDamage);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, brainDamage, 9, 'B'));
            var eclipse = Song.Create("Eclipse", pinkFloyd);
            _songRepository.AddSong(_dataSource, eclipse);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(darkSideOfTheMoon, eclipse, 10, 'B'));
            var rogerAlbumCredit = AlbumCredit.Create(darkSideOfTheMoon, rogerMember.Person);
            _albumRepository.AddAlbumCredit(_dataSource, rogerAlbumCredit);
            foreach (var role in rogerMember.Roles)
                _albumRepository.AddAlbumCreditRole(_dataSource, rogerAlbumCredit, role);
            var davidAlbumCredit = AlbumCredit.Create(darkSideOfTheMoon, davidMember.Person);
            _albumRepository.AddAlbumCredit(_dataSource, davidAlbumCredit);
            foreach (var role in davidMember.Roles)
                _albumRepository.AddAlbumCreditRole(_dataSource, davidAlbumCredit, role);
            var nickAlbumCredit = AlbumCredit.Create(darkSideOfTheMoon, nickMember.Person);
            _albumRepository.AddAlbumCredit(_dataSource, nickAlbumCredit);
            foreach (var role in nickMember.Roles)
                _albumRepository.AddAlbumCreditRole(_dataSource, nickAlbumCredit, role);
            var richardAlbumCredit = AlbumCredit.Create(darkSideOfTheMoon, richardMember.Person);
            _albumRepository.AddAlbumCredit(_dataSource, richardAlbumCredit);
            foreach (var role in richardMember.Roles)
                _albumRepository.AddAlbumCreditRole(_dataSource, richardAlbumCredit, role);
            var beatles = _artistRepository.GetArtist(_dataSource, _beatlesId);
            Assert.NotNull(beatles);
            _albumRepository.AddAlbumCollaborator(_dataSource, AlbumCollaborator.Create(darkSideOfTheMoon, beatles));
            var release = AlbumRelease.Create(darkSideOfTheMoon, DateTime.Parse("3/1/1971"), MediaTypes.Vinyl, harvest, "SHVL 804");
            _albumRepository.AddAlbumRelease(_dataSource, release);
            var record = AlbumMedia.Create(darkSideOfTheMoon, MediaTypes.Vinyl);
            record.Release = release;
            _albumRepository.AddAlbumMedia(_dataSource, record);

            darkSideOfTheMoon = _albumRepository.GetAlbum(_dataSource, darkSideOfTheMoonId);
            Assert.NotNull(darkSideOfTheMoon);
            Assert.AreEqual("The Dark Side of the Moon", darkSideOfTheMoon.Name);
            Assert.NotNull(darkSideOfTheMoon.Artist);
            Assert.AreEqual("Pink Floyd", darkSideOfTheMoon.Artist.Name);
            Assert.NotNull(darkSideOfTheMoon.Tracks);
            Assert.AreEqual(10, darkSideOfTheMoon.Tracks.Count);
            Assert.NotNull(darkSideOfTheMoon.Collaborators);
            Assert.AreEqual(1, darkSideOfTheMoon.Collaborators.Count);
            Assert.NotNull(darkSideOfTheMoon.Credits);
            Assert.AreEqual(4, darkSideOfTheMoon.Credits.Count);
            Assert.NotNull(darkSideOfTheMoon.Credits[0]);
            Assert.AreEqual("David Gilmour", darkSideOfTheMoon.Credits[0].Person.Name);
            Assert.NotNull(darkSideOfTheMoon.Credits[0].Roles);
            Assert.AreEqual(4, darkSideOfTheMoon.Credits[0].Roles.Count);
            Assert.NotNull(darkSideOfTheMoon.Media);
            Assert.AreEqual(MediaTypes.Vinyl, darkSideOfTheMoon.Media[0].MediaType);
            Assert.NotNull(darkSideOfTheMoon.Media[0].Release);
            Assert.AreEqual(harvest.Id, darkSideOfTheMoon.Media[0].Release.LabelId);
            Assert.AreEqual("SHVL 804", darkSideOfTheMoon.Media[0].Release.CatalogNumber);
            Assert.AreEqual(DateTime.Parse("3/1/1971"), darkSideOfTheMoon.Media[0].Release.ReleaseDate);

            _albumRepository.DeleteAlbum(_dataSource, darkSideOfTheMoon);

            darkSideOfTheMoon = _albumRepository.GetAlbum(_dataSource, darkSideOfTheMoonId);
            Assert.IsNull(darkSideOfTheMoon);
        }
    }
}
