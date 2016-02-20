using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Bloom.Common.Tests
{
    /// <summary>
    /// Tests for the RegExPattern class.
    /// </summary>
    [TestFixture]
    public class RegExPatternTests
    {
        /// <summary>
        /// Tests the alphabetic regex pattern.
        /// </summary>
        [Test]
        public void AlphabeticPatternTest()
        {
            const string valid = "alphabeticTEST";
            const string invalid1 = "alphabetic TEST";
            const string invalid2 = "alphabetic123";
            const string invalid3 = "alphabetic%!!";

            Assert.IsTrue(Regex.IsMatch(valid, RegExPattern.Alphabetic));
            Assert.IsFalse(Regex.IsMatch(invalid1, RegExPattern.Alphabetic));
            Assert.IsFalse(Regex.IsMatch(invalid2, RegExPattern.Alphabetic));
            Assert.IsFalse(Regex.IsMatch(invalid3, RegExPattern.Alphabetic));
        }

        /// <summary>
        /// Tests the alphabetic with whitespace regex pattern.
        /// </summary>
        [Test]
        public void AlphabeticWithWhitespacePatternTest()
        {
            const string valid1 = "alphabeticTEST";
            const string valid2 = "alphabetic TEST";
            const string invalid1 = "alphabetic123";
            const string invalid2 = "alphabetic%!!";

            Assert.IsTrue(Regex.IsMatch(valid1, RegExPattern.AlphabeticWithWhitespace));
            Assert.IsTrue(Regex.IsMatch(valid2, RegExPattern.AlphabeticWithWhitespace));
            Assert.IsFalse(Regex.IsMatch(invalid1, RegExPattern.AlphabeticWithWhitespace));
            Assert.IsFalse(Regex.IsMatch(invalid2, RegExPattern.AlphabeticWithWhitespace));
        }

        /// <summary>
        /// Tests the alphanumeric regex pattern.
        /// </summary>
        [Test]
        public void AlphaNumericPatternTest()
        {
            const string valid1 = "alphabeticTEST";
            const string valid2 = "alphabetic123";
            const string invalid1 = "alphabetic 123";
            const string invalid2 = "alphabetic TEST";
            const string invalid3 = "alphabetic%!!";

            Assert.IsTrue(Regex.IsMatch(valid1, RegExPattern.AlphaNumeric));
            Assert.IsTrue(Regex.IsMatch(valid2, RegExPattern.AlphaNumeric));
            Assert.IsFalse(Regex.IsMatch(invalid1, RegExPattern.AlphaNumeric));
            Assert.IsFalse(Regex.IsMatch(invalid2, RegExPattern.AlphaNumeric));
            Assert.IsFalse(Regex.IsMatch(invalid3, RegExPattern.AlphaNumeric));
        }

        /// <summary>
        /// Tests the alphanumeric with whitespace regex pattern.
        /// </summary>
        [Test]
        public void AlphaNumericWithWhitespacePatternTest()
        {
            const string valid1 = "alphabeticTEST";
            const string valid2 = "alphabetic123";
            const string valid3 = "alphabetic 123";
            const string valid4 = "alphabetic TEST";
            const string invalid3 = "alphabetic%!!";

            Assert.IsTrue(Regex.IsMatch(valid1, RegExPattern.AlphaNumericWithWhitespace));
            Assert.IsTrue(Regex.IsMatch(valid2, RegExPattern.AlphaNumericWithWhitespace));
            Assert.IsTrue(Regex.IsMatch(valid3, RegExPattern.AlphaNumericWithWhitespace));
            Assert.IsTrue(Regex.IsMatch(valid4, RegExPattern.AlphaNumericWithWhitespace));
            Assert.IsFalse(Regex.IsMatch(invalid3, RegExPattern.AlphaNumericWithWhitespace));
        }

        /// <summary>
        /// Tests the numeric regex pattern.
        /// </summary>
        [Test]
        public void NumericPatternTest()
        {
            const string valid = "123";
            const string invalid1 = "6.159";
            const string invalid2 = "5%";
            const string invalid3 = "Six";
            const string invalid4 = "44 44";

            Assert.IsTrue(Regex.IsMatch(valid, RegExPattern.Numeric));
            Assert.IsFalse(Regex.IsMatch(invalid1, RegExPattern.Numeric));
            Assert.IsFalse(Regex.IsMatch(invalid2, RegExPattern.Numeric));
            Assert.IsFalse(Regex.IsMatch(invalid3, RegExPattern.Numeric));
            Assert.IsFalse(Regex.IsMatch(invalid4, RegExPattern.Numeric));
        }

        /// <summary>
        /// Tests the numeric with whitespace regex pattern.
        /// </summary>
        [Test]
        public void NumericWithWhitespacePatternTest()
        {
            const string valid1 = "123";
            const string valid2 = "44 44";
            const string invalid1 = "6.159";
            const string invalid2 = "5%";
            const string invalid3 = "Six";

            Assert.IsTrue(Regex.IsMatch(valid1, RegExPattern.NumericWithWhitespace));
            Assert.IsTrue(Regex.IsMatch(valid2, RegExPattern.NumericWithWhitespace));
            Assert.IsFalse(Regex.IsMatch(invalid1, RegExPattern.NumericWithWhitespace));
            Assert.IsFalse(Regex.IsMatch(invalid2, RegExPattern.NumericWithWhitespace));
            Assert.IsFalse(Regex.IsMatch(invalid3, RegExPattern.NumericWithWhitespace));
        }

        /// <summary>
        /// Tests the guid regex pattern.
        /// </summary>
        [Test]
        public void Guid()
        {
            var valid1 = System.Guid.NewGuid();
            const string valid2 = "51668e1e-631a-4dfa-9afc-382edcce3834";
            const string invalid1 = "20838242fe4b45da9a84d2b40eaf1b93";
            const string invalid2 = "af3d6e88-c8e0-488c-afc48-9eecf41bcaf";
            const string invalid3 = "af3d6e88fefefef";

            Assert.IsTrue(Regex.IsMatch(valid1.ToString(), RegExPattern.Guid));
            Assert.IsTrue(Regex.IsMatch(valid2, RegExPattern.Guid));
            Assert.IsFalse(Regex.IsMatch(invalid1, RegExPattern.Guid));
            Assert.IsFalse(Regex.IsMatch(invalid2, RegExPattern.Guid));
            Assert.IsFalse(Regex.IsMatch(invalid3, RegExPattern.Guid));
        }
    }
}
