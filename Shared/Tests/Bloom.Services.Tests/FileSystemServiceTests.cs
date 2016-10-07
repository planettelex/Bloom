using System.IO;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.Services.Tests
{
    /// <summary>
    /// Tests the file system service class.
    /// </summary>
    [TestFixture]
    public class FileSystemServiceTests
    {
        private User _testUser;
        private IFileSystemService _fileSystemService;
        private string _userProfilesFolder;
        private string _testFolder;
        private string _subFolder1;
        private string _subFolder2;
        private string _nestedFolder;
        private const string MediaFolder = "..\\..\\TestMedia\\";
        private const string NonMediaTxt = "Non-Media.txt";
        // Test image files
        private const string RonaldReaganJpg = "Ronald Reagan.jpg";
        // Test mp3 files
        private const string SomethingMp3 = "Something.mp3";
        private const string JungleBoogieMp3 = "Jungle Boogie.mp3";
        private const string BloomMp3 = "Bloom.mp3";
        // Test flac files
        private const string GuessAgainFlac = "Guess Again!.flac";
        // Test aac files
        private const string DiktatorOfTheFreeWorldAac = "Diktator of the Free World.m4a";
        private const string HandsInTheRainAac = "Hands in the Rain.m4a";
        // Test folders
        private const string RootFolder = "File System Tests";
        private const string SubFolder1 = "Sub Folder 1";
        private const string NestedFolder = "Nested Folder";
        private const string SubFolder2 = "Sub Folder 2";

        /// <summary>
        /// Sets up the tests.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _testUser = User.Create(Person.Create("File System Tester"));

            _userProfilesFolder = Settings.UserProfilesPath;
            if (!Directory.Exists(_userProfilesFolder))
                Directory.CreateDirectory(_userProfilesFolder);

            _fileSystemService = new FileSystemService();
            _testFolder = Path.Combine(Data.Settings.TestsDataPath, RootFolder);
            if (!Directory.Exists(_testFolder))
                Directory.CreateDirectory(_testFolder);

            _subFolder1 = Path.Combine(_testFolder, SubFolder1);
            if (!Directory.Exists(_subFolder1))
                Directory.CreateDirectory(_subFolder1);

            _nestedFolder = Path.Combine(_subFolder1, NestedFolder);
            if (!Directory.Exists(_nestedFolder))
                Directory.CreateDirectory(_nestedFolder);

            _subFolder2 = Path.Combine(_testFolder, SubFolder2);
            if (!Directory.Exists(_subFolder2))
                Directory.CreateDirectory(_subFolder2);

            // Copy test media to nested folder stucture.
            File.Copy(MediaFolder + BloomMp3, Path.Combine(_testFolder, BloomMp3), true);
            File.Copy(MediaFolder + SomethingMp3, Path.Combine(_testFolder, SomethingMp3), true);
            File.Copy(MediaFolder + DiktatorOfTheFreeWorldAac, Path.Combine(_subFolder1, DiktatorOfTheFreeWorldAac), true);
            File.Copy(MediaFolder + JungleBoogieMp3, Path.Combine(_nestedFolder, JungleBoogieMp3), true);
            File.Copy(MediaFolder + GuessAgainFlac, Path.Combine(_subFolder2, GuessAgainFlac), true);
            File.Copy(MediaFolder + HandsInTheRainAac, Path.Combine(_subFolder2, HandsInTheRainAac), true);

            // Copy some non media files to nested folder structure.
            File.Copy(MediaFolder + RonaldReaganJpg, Path.Combine(_subFolder1, RonaldReaganJpg), true);
            File.Copy(MediaFolder + NonMediaTxt, Path.Combine(_nestedFolder, NonMediaTxt), true);
        }

        /// <summary>
        /// Tests copying the profile image.
        /// </summary>
        [Test]
        public void CopyProfileImageTest()
        {
            const string imagePath = MediaFolder + RonaldReaganJpg;
            var destination = _fileSystemService.CopyProfileImage(_testUser, imagePath);

            Assert.That(destination.StartsWith(Path.Combine(Settings.UserProfilesPath, _testUser.PersonId.ToString().Replace("-", ""))));
            Assert.IsTrue(File.Exists(destination));
            File.Delete(destination);
        }

        /// <summary>
        /// Tests listing the music files.
        /// </summary>
        [Test]
        public void ListMusicFilesTest()
        {
            var musicFiles = _fileSystemService.ListMusicFiles(_testFolder);
            Assert.NotNull(musicFiles);
            Assert.AreEqual(6, musicFiles.Count);
        }
    }
}
