using Bloom.Common.ExtensionMethods;
using NUnit.Framework;

namespace Bloom.Common.Tests.ExtensionMethods
{
    /// <summary>
    /// Tests for the string extension methods.
    /// </summary>
    [TestFixture]
    public class StringExtensionTests
    {
        /// <summary>
        /// Tests whether a string is a valid filename.
        /// </summary>
        [Test]
        public void IsValidFilenameTest()
        {
            const string longString = "Lorem ipsum dolor sit amet consectetuer adipiscing elit Aenean commodo ligula eget dolor Aenean massa Aenean commodo ligula Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus Donec quam felis ultricies nec pellentesque eu pretium quis sem.gif";
            const string invalid1 = "file*.*";
            const string invalid2 = "folder\\path";
            const string invalid3 = "<tag>";
            const string invalid4 = "file?.txt";
            const string invalid5 = "file|name.txt";
            const string valid1 = "file.txt";
            const string valid2 = "file.name.txt";
            const string valid3 = "my dad's file.jpg";

            Assert.IsFalse(longString.IsValidFileName());
            Assert.IsFalse(invalid1.IsValidFileName());
            Assert.IsFalse(invalid2.IsValidFileName());
            Assert.IsFalse(invalid3.IsValidFileName());
            Assert.IsFalse(invalid4.IsValidFileName());
            Assert.IsFalse(invalid5.IsValidFileName());
            Assert.IsTrue(valid1.IsValidFileName());
            Assert.IsTrue(valid2.IsValidFileName());
            Assert.IsTrue(valid3.IsValidFileName());
        }
    }
}
