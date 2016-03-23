using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the review repository class.
    /// </summary>
    [TestFixture]
    public class ReviewRepositoryTests
    {
        private const string TestFileName = "ReviewRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private ISourceRepository _sourceRepository;
        private IReviewRepository _reviewRepository;
        private Guid _nevermindReviewId;
        private Guid _smellsLikeTeenSpiritReviewId;
        private Song _smellsLikeTeenSpirit;
        private Album _nevermind;

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
            _songRepository = new SongRepository(_roleRepository, _personRepository);
            _albumRepository = new AlbumRepository(_roleRepository, _personRepository);
            _sourceRepository = new SourceRepository();
            _reviewRepository = new ReviewRepository(_personRepository, _sourceRepository);

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
            var nirvana = Artist.Create("Nirvana");
            _artistRepository.AddArtist(_dataSource, nirvana);

            _smellsLikeTeenSpirit = Song.Create("Smells Like Teen Spirit", nirvana);
            _songRepository.AddSong(_dataSource, _smellsLikeTeenSpirit);

            _nevermind = Album.Create("Nevermind", nirvana);
            _albumRepository.AddAlbum(_dataSource, _nevermind);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_nevermind, _smellsLikeTeenSpirit, 1));

            var rollingStone = Source.Create("Rolling Stone");
            var spin = Source.Create("Spin");
            var gutterBubbles = Source.Create("Gutterbubbles");

            var nevermindReview1 = Review.Create(gutterBubbles, "http://www.gutterbubbles.com/nevermind-review");
            _reviewRepository.AddReview(_dataSource, nevermindReview1);
            _reviewRepository.AddReviewTo(_dataSource, nevermindReview1, _nevermind);
            var nevermindReview2 = Review.Create(rollingStone, "With the Lights Out, Entertain Us", "Nevermind Rolling Stone Review", Person.Create("Jane Doe"));
            _nevermindReviewId = nevermindReview2.Id;
            nevermindReview2.PublishedOn = DateTime.Parse("10/20/1991");
            _reviewRepository.AddReview(_dataSource, nevermindReview2);
            _reviewRepository.AddReviewTo(_dataSource, nevermindReview2, _nevermind);

            var smellsLikeTeenSpiritReview1 = Review.Create(gutterBubbles, "http://www.gutterbubbles.com/teen-spirit-review");
            _smellsLikeTeenSpiritReviewId = smellsLikeTeenSpiritReview1.Id;
            _reviewRepository.AddReview(_dataSource, smellsLikeTeenSpiritReview1);
            _reviewRepository.AddReviewTo(_dataSource, smellsLikeTeenSpiritReview1, _smellsLikeTeenSpirit);
            var smellsLikeTeenSpiritReview2 = Review.Create(spin, "Hello Hello How Low?", "Smells Like Teen Spirit Spin Review", Person.Create("John Doe"));
            smellsLikeTeenSpiritReview2.PublishedOn = DateTime.Parse("10/30/1991");
            _reviewRepository.AddReview(_dataSource, smellsLikeTeenSpiritReview2);
            _reviewRepository.AddReviewTo(_dataSource, smellsLikeTeenSpiritReview2, _smellsLikeTeenSpirit);
        }

        /// <summary>
        /// Tests the get review method.
        /// </summary>
        [Test]
        public void GetReviewTests()
        {
            var nevermindReview = _reviewRepository.GetReview(_dataSource, _nevermindReviewId);
            Assert.NotNull(nevermindReview);
            Assert.AreEqual(_nevermindReviewId, nevermindReview.Id);
            Assert.AreEqual("With the Lights Out, Entertain Us", nevermindReview.Title);
            Assert.AreEqual("Nevermind Rolling Stone Review", nevermindReview.Body);
            Assert.NotNull(nevermindReview.Source);
            Assert.AreEqual("Rolling Stone", nevermindReview.Source.Name);
            Assert.NotNull(nevermindReview.Author);
            Assert.AreEqual("Jane Doe", nevermindReview.Author.Name);
            Assert.AreEqual(DateTime.Parse("10/20/1991"), nevermindReview.PublishedOn);

            var smellsLikeTeenSpiritReview = _reviewRepository.GetReview(_dataSource, _smellsLikeTeenSpiritReviewId);
            Assert.NotNull(smellsLikeTeenSpiritReview);
            Assert.AreEqual(_smellsLikeTeenSpiritReviewId, smellsLikeTeenSpiritReview.Id);
            Assert.AreEqual("http://www.gutterbubbles.com/teen-spirit-review", smellsLikeTeenSpiritReview.Url);
            Assert.NotNull(smellsLikeTeenSpiritReview.Source);
            Assert.AreEqual("Gutterbubbles", smellsLikeTeenSpiritReview.Source.Name);
            Assert.IsNull(smellsLikeTeenSpiritReview.Author);
            Assert.IsNull(smellsLikeTeenSpiritReview.PublishedOn);
        }

        /// <summary>
        /// Tests the list song reviews method.
        /// </summary>
        [Test]
        public void ListSongReviewsTest()
        {
            var reviews = _reviewRepository.ListReviews(_dataSource, _smellsLikeTeenSpirit);
            Assert.NotNull(reviews);
            Assert.AreEqual(2, reviews.Count);
            Assert.AreEqual("Hello Hello How Low?", reviews[0].Title);
            Assert.AreEqual("Smells Like Teen Spirit Spin Review", reviews[0].Body);
            Assert.AreEqual(DateTime.Parse("10/30/1991"), reviews[0].PublishedOn);
            Assert.NotNull(reviews[0].Author);
            Assert.AreEqual("John Doe", reviews[0].Author.Name);
            Assert.NotNull(reviews[0].Source);
            Assert.AreEqual("Spin", reviews[0].Source.Name);
            Assert.AreEqual("http://www.gutterbubbles.com/teen-spirit-review", reviews[1].Url);
            Assert.NotNull(reviews[1].Source);
            Assert.AreEqual("Gutterbubbles", reviews[1].Source.Name);
            Assert.IsNull(reviews[1].Author);
        }

        /// <summary>
        /// Tests the list album reviews method.
        /// </summary>
        [Test]
        public void ListAlbumReviewsTest()
        {
            var reviews = _reviewRepository.ListReviews(_dataSource, _nevermind);
            Assert.NotNull(reviews);
            Assert.AreEqual(2, reviews.Count);
            Assert.AreEqual("With the Lights Out, Entertain Us", reviews[0].Title);
            Assert.AreEqual("Nevermind Rolling Stone Review", reviews[0].Body);
            Assert.AreEqual(DateTime.Parse("10/20/1991"), reviews[0].PublishedOn);
            Assert.NotNull(reviews[0].Author);
            Assert.AreEqual("Jane Doe", reviews[0].Author.Name);
            Assert.NotNull(reviews[0].Source);
            Assert.AreEqual("Rolling Stone", reviews[0].Source.Name);
            Assert.AreEqual("http://www.gutterbubbles.com/nevermind-review", reviews[1].Url);
            Assert.NotNull(reviews[1].Source);
            Assert.AreEqual("Gutterbubbles", reviews[1].Source.Name);
            Assert.IsNull(reviews[1].Author);
        }

        /// <summary>
        /// Tests the delete review from song method.
        /// </summary>
        [Test]
        public void DeleteReviewFromSongTest()
        {
            var reviews = _reviewRepository.ListReviews(_dataSource, _smellsLikeTeenSpirit);
            Assert.NotNull(reviews);
            Assert.AreEqual(2, reviews.Count);

            var westword = Source.Create("Westword");
            var smellsLikeTeenSpiritReview3 = Review.Create(westword, "Whatever", "Smells Like Teen Spirit Westword Review", Person.Create("Sam Smith"));
            smellsLikeTeenSpiritReview3.PublishedOn = DateTime.Parse("1/15/1992");
            _reviewRepository.AddReview(_dataSource, smellsLikeTeenSpiritReview3);
            _reviewRepository.AddReviewTo(_dataSource, smellsLikeTeenSpiritReview3, _smellsLikeTeenSpirit);

            reviews = _reviewRepository.ListReviews(_dataSource, _smellsLikeTeenSpirit);
            Assert.AreEqual(3, reviews.Count);
            Assert.AreEqual(smellsLikeTeenSpiritReview3.Id, reviews[0].Id);

            _reviewRepository.DeleteReviewFrom(_dataSource, reviews[0], _smellsLikeTeenSpirit);

            reviews = _reviewRepository.ListReviews(_dataSource, _smellsLikeTeenSpirit);
            Assert.AreEqual(2, reviews.Count);
        }

        /// <summary>
        /// Tests the delete review from album method.
        /// </summary>
        [Test]
        public void DeleteReviewFromAlbumTest()
        {
            var reviews = _reviewRepository.ListReviews(_dataSource, _nevermind);
            Assert.NotNull(reviews);
            Assert.AreEqual(2, reviews.Count);

            var pitchfork = Source.Create("Pitchfork");
            var nevermindReview3 = Review.Create(pitchfork, "In Bloom", "Nevermind Pitchfork Review", Person.Create("Sally Smith"));
            nevermindReview3.PublishedOn = DateTime.Parse("1/20/1992");
            _reviewRepository.AddReview(_dataSource, nevermindReview3);
            _reviewRepository.AddReviewTo(_dataSource, nevermindReview3, _nevermind);

            reviews = _reviewRepository.ListReviews(_dataSource, _nevermind);
            Assert.AreEqual(3, reviews.Count);
            Assert.AreEqual(nevermindReview3.Id, reviews[0].Id);

            _reviewRepository.DeleteReviewFrom(_dataSource, reviews[0], _nevermind);

            reviews = _reviewRepository.ListReviews(_dataSource, _nevermind);
            Assert.AreEqual(2, reviews.Count);
        }

        /// <summary>
        /// Tests the delete review method.
        /// </summary>
        [Test]
        public void DeleteReviewTest()
        {
            var billboard = Source.Create("Billboard");
            var newReview = Review.Create(billboard, "Holy Nirvana!", "Billboard Nevermind Review", Person.Create("Pam Patterson"));
            newReview.PublishedOn = DateTime.Parse("2/2/1992");
            _reviewRepository.AddReview(_dataSource, newReview);
            _reviewRepository.AddReviewTo(_dataSource, newReview, _nevermind);
            _reviewRepository.AddReviewTo(_dataSource, newReview, _smellsLikeTeenSpirit);

            var reviews = _reviewRepository.ListReviews(_dataSource, _nevermind);
            Assert.AreEqual(3, reviews.Count);
            reviews = _reviewRepository.ListReviews(_dataSource, _smellsLikeTeenSpirit);
            Assert.AreEqual(3, reviews.Count);

            _reviewRepository.DeleteReview(_dataSource, newReview);

            reviews = _reviewRepository.ListReviews(_dataSource, _nevermind);
            Assert.AreEqual(2, reviews.Count);
            reviews = _reviewRepository.ListReviews(_dataSource, _smellsLikeTeenSpirit);
            Assert.AreEqual(2, reviews.Count);
        }
    }
}
